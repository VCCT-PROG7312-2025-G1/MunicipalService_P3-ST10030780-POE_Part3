using Microsoft.AspNetCore.Mvc;
namespace MunicipalService_P3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Help() => View();
    }
}