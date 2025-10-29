using System.Diagnostics;
using DemoWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Contact form - GET
        public IActionResult Contact()
        {
            // Check for success flag in TempData after PRG
            if (TempData.ContainsKey("ContactSuccess") && TempData["ContactSuccess"].ToString() == "true")
            {
                ViewBag.Success = true;
            }

            return View(new ContactViewModel());
        }

        // Contact form - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Here you would typically send email or store the inquiry. We'll log it for now.
            _logger.LogInformation("New contact submission from {Name} <{Email}>: {Message}", model.Name ?? "(no name)", model.Email, model.Message);

            // Use PRG pattern to avoid form resubmission
            TempData["ContactSuccess"] = "true";
            return RedirectToAction("Contact");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
