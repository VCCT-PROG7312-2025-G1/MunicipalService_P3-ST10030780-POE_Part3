using Microsoft.AspNetCore.Mvc;
using MunicipalService_P3.Models;
using MunicipalService_P3.Data;

namespace MunicipalService_P3.Controllers
{
    public class IssuesController : Controller
    {
        private readonly AppDbContext _context;

        public IssuesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Report()
        {
            return View(new Issue());
        }

        [HttpPost]
        public IActionResult Report(Issue issue)
        {
            if (!ModelState.IsValid)
                return View(issue);

            _context.Issues.Add(issue);
            _context.SaveChanges();

            ViewBag.Message = "Issue submitted successfully.";
            return View(new Issue());
        }

        [HttpGet]
        public IActionResult List()
        {
            var issues = _context.Issues.OrderByDescending(i => i.Timestamp).ToList();
            return View(issues);
        }
    }
}