using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SetStats.Web.Models;

namespace SetStats.Web.Controllers;

public class HomeController : Controller
{
    public HomeController() { }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult Privacy() => View();

    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
