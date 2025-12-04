using Microsoft.AspNetCore.Mvc;
using MunicipalService_P3.Services;

namespace MunicipalService_P3.Controllers
{
    public class EventsController : Controller
    {
        private readonly IDataService _data;
        public EventsController(IDataService data) => _data = data;

        public IActionResult Index(string? keyword, string? category, DateTime? date)
        {
            ViewBag.Categories = _data.GetEventCategories();
            ViewBag.Recommendations = _data.GetRecommendations(keyword, category);
            return View(_data.GetEvents(keyword, category, date));
        }
    }
}