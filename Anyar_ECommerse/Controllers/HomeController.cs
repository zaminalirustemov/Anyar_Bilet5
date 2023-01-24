
using Anyar_ECommerse.Context;
using Anyar_ECommerse.VievModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anyar_ECommerse.Controllers;

public class HomeController : Controller
{
    private readonly AnyarDbContext _anyarDbContext;

    public HomeController(AnyarDbContext anyarDbContext)
    {
        _anyarDbContext = anyarDbContext;
    }
    public IActionResult Index()
    {
        HomeViewModel homeViewModel = new HomeViewModel
        {
            Teams=_anyarDbContext.Teams.Include(x=>x.Position).Where(x=>x.IsDelete==false).ToList(),
        };
        return View(homeViewModel);
    }

}
