using Microsoft.AspNetCore.Mvc;

namespace KaseyWebApi.Controllers;

public class MainController : Controller
{
    // GET /<controller>/
    public IActionResult Index()
    {
        return new RedirectResult("~/swagger/");
    }
}