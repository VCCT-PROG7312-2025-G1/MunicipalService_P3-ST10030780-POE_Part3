using Microsoft.AspNetCore.Mvc;
using MunicipalService_P3.Models;
using MunicipalService_P3.Services;

namespace MunicipalService_P3.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IDataService _data;
        public IssuesController(IDataService data) => _data = data;

        [HttpGet]
        public IActionResult Report() => View();

        [HttpPost]
        public IActionResult Report(Issue issue)
        {
            if (string.IsNullOrWhiteSpace(issue.Description))
                ModelState.AddModelError(nameof(issue.Description), "Description is required.");

            if (!ModelState.IsValid) return View(issue);

            _data.AddIssue(issue);
            ViewBag.Message = "Issue submitted successfully.";
            return View();
        }
    }
}