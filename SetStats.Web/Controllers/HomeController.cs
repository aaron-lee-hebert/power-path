using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SetStats.Web.Models;

namespace SetStats.Web.Controllers;

public class HomeController : Controller
{
    public HomeController() { }

    public IActionResult Index() => View();

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
