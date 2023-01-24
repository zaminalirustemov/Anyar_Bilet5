using Anyar_ECommerse.Context;
using Anyar_ECommerse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Anyar_ECommerse.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "SuperAdmin,Admin,Editor")]
public class TeamController : Controller
{
    private readonly AnyarDbContext _anyarDbContext;

    public TeamController(AnyarDbContext anyarDbContext)
    {
        _anyarDbContext = anyarDbContext;
    }
    public IActionResult Index()
    {
        List<Team> teams = _anyarDbContext.Teams.Include(x=>x.Position).Where(x => x.IsDelete == false).ToList();
        return View(teams);
    }
    //CREATE---------------------------------------------------------------------------------
    public IActionResult Create()
    {
        ViewBag.Position = _anyarDbContext.Positions.Where(x=>x.IsDelete==false).ToList();

        return View();
    }
    [HttpPost]
    public IActionResult Create(Team team)
    {
        ViewBag.Position = _anyarDbContext.Positions.Where(x => x.IsDelete == false).ToList();

        if (!ModelState.IsValid) return View(team);

        if (team.ImageFile is null)
        {

            ModelState.AddModelError("ImageFile", "bos ola bilmez.");
            return View(team);
        }
        if (team.ImageFile.ContentType != "image/jpeg" && team.ImageFile.ContentType != "image/png")
        {
            ModelState.AddModelError("ImageFile", "You can only upload jpeg and png file.");
            return View(team);
        }
        if (team.ImageFile.Length> 2097152)
        {
            ModelState.AddModelError("ImageFile", "You can only upload jpeg and png file.");
            return View(team);
        }

        string filename=team.ImageFile.FileName;

        if (filename.Length > 64) filename = filename.Substring(filename.Length - 64,64) ;

        filename=Guid.NewGuid()+filename;
        string path = "C:\\Users\\User\\Desktop\\Anyar_ZR\\Anyar_ECommerse\\wwwroot\\uploads\\team\\" + filename;

        using (FileStream fileStream = new FileStream(path,FileMode.Create))
        {
            team.ImageFile.CopyTo(fileStream);
        }
        team.ImageName=filename;


        _anyarDbContext.Teams.Add(team);
        _anyarDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    //UPDATE---------------------------------------------------------------------------------
    public IActionResult Update(int id)
    {
        ViewBag.Position = _anyarDbContext.Positions.Where(x => x.IsDelete == false).ToList();
        Team team = _anyarDbContext.Teams.FirstOrDefault(x => x.Id == id);
        if (team is null) return NotFound();

        return View(team);
    }
    [HttpPost]
    public IActionResult Update(Team newTeam)
    {
        Team existTeam = _anyarDbContext.Teams.FirstOrDefault(x => x.Id == newTeam.Id);
        if (existTeam is null) return NotFound();

        if (!ModelState.IsValid) return View(newTeam);


        if (newTeam.ImageFile!=null)
        {
            if (newTeam.ImageFile.ContentType != "image/jpeg" && newTeam.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "You can only upload jpeg and png file.");
                return View(newTeam);
            }
            if (newTeam.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "You can only upload jpeg and png file.");
                return View(newTeam);
            }

            string filename = newTeam.ImageFile.FileName;

            if (filename.Length > 64) filename = filename.Substring(filename.Length - 64, 64);

            filename = Guid.NewGuid() + filename;
            string path = "C:\\Users\\User\\Desktop\\Anyar_ZR\\Anyar_ECommerse\\wwwroot\\uploads\\team\\" + filename;

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                newTeam.ImageFile.CopyTo(fileStream);
            }
            existTeam.ImageName = filename;
        }

        existTeam.PositionId=newTeam.PositionId;
        existTeam.Name = newTeam.Name;
        existTeam.Description = newTeam.Description;
        existTeam.FbUrl=newTeam.FbUrl;
        existTeam.TwUrl=newTeam.TwUrl;
        existTeam.InstUrl=newTeam.InstUrl;
        existTeam.InUrl=newTeam.InUrl;

        _anyarDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    //SoftDELETE---------------------------------------------------------------------------------
    public IActionResult SoftDelete(int id)
    {
        Team team = _anyarDbContext.Teams.FirstOrDefault(x => x.Id == id);
        if (team is null) return NotFound();

        team.IsDelete = true;
        _anyarDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
