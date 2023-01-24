using Anyar_ECommerse.Context;
using Anyar_ECommerse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Anyar_ECommerse.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles ="SuperAdmin,Admin,Editor")]
public class DashboardController : Controller
{
    private readonly AnyarDbContext _anyarDbContext;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DashboardController(AnyarDbContext anyarDbContext,UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _anyarDbContext = anyarDbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public IActionResult Index()
    {
        return View();
    }

    //CreateAdmin----------------------------------------------------------------
    public async Task<IActionResult> CreateAdmin()
    {
        AppUser appUser = new AppUser
        {
            UserName = "Zaminali",
            Fullname = "Zamin123",
            Email = "xanim@mail.ru",

        };
        var user = await _userManager.CreateAsync(appUser,"Zamin123");

        return Ok(user);

    }

    //CreateRole-------------------------------------------------------------------
    public async Task<IActionResult> CreateRole()
    {
        IdentityRole role1 = new IdentityRole("SuperAdmin");
        IdentityRole role2 = new IdentityRole("Admin");
        IdentityRole role3 = new IdentityRole("Editor");
        IdentityRole role4 = new IdentityRole("Member");

        await _roleManager.CreateAsync(role1);
        await _roleManager.CreateAsync(role2);
        await _roleManager.CreateAsync(role3);
        await _roleManager.CreateAsync(role4);

        return Content("Created role");
    }

    //CreateRole-------------------------------------------------------------------
    public async Task<IActionResult> AddRole()
    {
        AppUser appUser = await _userManager.FindByNameAsync("Zaminali");

        await _userManager.AddToRoleAsync(appUser, "SuperAdmin");

        return Content("Role menimsedildi");
    }
}

