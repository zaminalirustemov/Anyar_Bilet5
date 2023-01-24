using Anyar_ECommerse.Context;
using Anyar_ECommerse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Anyar_ECommerse.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "SuperAdmin,Admin,Editor")]
public class PositonController : Controller
{
    private readonly AnyarDbContext _anyarDbContext;

    public PositonController(AnyarDbContext anyarDbContext)
    {
        _anyarDbContext = anyarDbContext;
    }
    //READ---------------------------------------------------------------------------------
    public IActionResult Index()
    {
        List<Position> positions = _anyarDbContext.Positions.Where(x=>x.IsDelete==false).ToList();
        return View(positions);
    }
    //CREATE---------------------------------------------------------------------------------
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Position position)
    {
        if (!ModelState.IsValid) return View(position);

        _anyarDbContext.Positions.Add(position);
        _anyarDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
    //UPDATE---------------------------------------------------------------------------------
    public IActionResult Update(int id)
    {
        Position position = _anyarDbContext.Positions.FirstOrDefault(x=>x.Id==id);
        if(position is null) return NotFound();

        return View(position);
    }
    [HttpPost]
    public IActionResult Update(Position newPosition)
    {
        Position existPosition = _anyarDbContext.Positions.FirstOrDefault(x => x.Id == newPosition.Id);
        if (existPosition is null) return NotFound();

        if (!ModelState.IsValid) return View(newPosition); 

        existPosition.Name=newPosition.Name;
        _anyarDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    //SoftDELETE---------------------------------------------------------------------------------
    public IActionResult SoftDelete(int id)
    {
        Position position = _anyarDbContext.Positions.FirstOrDefault(x => x.Id == id);
        if (position is null) return NotFound();

        position.IsDelete=true;
        _anyarDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
