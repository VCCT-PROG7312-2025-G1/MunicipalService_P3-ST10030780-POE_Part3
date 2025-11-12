using Microsoft.AspNetCore.Mvc;
using MunicipalService_P3.Models;
using MunicipalService_P3.Services;

namespace MunicipalService_P3.Controllers
{
    public class StatusController : Controller
    {
        private readonly IDataService _data;

        public StatusController(IDataService data) => _data = data;

        [HttpGet]
        public IActionResult Index()
        {
            var all = _data.GetAllServiceRequests();
            ViewBag.TopPriority = _data.GetTopPriority();
            return View(all);
        }

        [HttpPost]
        public IActionResult Search(int requestId)
        {
            ViewBag.Result = _data.FindRequest(requestId);
            ViewBag.Dependencies = _data.GetDependencies(requestId);
            ViewBag.TopPriority = _data.GetTopPriority();
            return View("Index", _data.GetAllServiceRequests());
        }

        [HttpPost]
        public IActionResult Create(ServiceRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Description))
                ModelState.AddModelError(nameof(req.Description), "Description is required.");

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid input.";
                return RedirectToAction("Index");
            }

            var id = _data.EnqueueServiceRequest(req);
            TempData["Created"] = $"Request {id} created.";
            return RedirectToAction("Index");
        }
    }
}