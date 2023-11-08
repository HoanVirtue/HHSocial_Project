using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Clone_Main_Project_0710.Models;
using Clone_Main_Project_0710.DataSession;

namespace Clone_Main_Project_0710.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // Home
    public IActionResult Index()
    {
        // string emailSess = HttpContext.Session.GetString(SessionData.USER_EMAIL_SESS);
        // if (emailSess == null)
        // {
        //     return RedirectToAction("Index", "Users");
        // }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
