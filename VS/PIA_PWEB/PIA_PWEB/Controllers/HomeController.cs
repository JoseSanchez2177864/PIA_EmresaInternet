using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PIA_PWEB.Models;
using PIA_PWEB.Models.dbModels;
using System.Diagnostics;

namespace PIA_PWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                ViewBag.UserRole = roles.FirstOrDefault(); // Pasamos el rol al ViewBag
            }
            return View();
        }
        public async Task<IActionResult> Nosotros()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                ViewBag.UserRole = roles.FirstOrDefault(); // Pasamos el rol al ViewBag
            }
            return View();
        } 
        public async Task<IActionResult> Contact()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                ViewBag.UserRole = roles.FirstOrDefault(); // Pasamos el rol al ViewBag
            }
            return View();
        } 
        public async Task<IActionResult> Estadisticas()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                ViewBag.UserRole = roles.FirstOrDefault(); // Pasamos el rol al ViewBag
            }
            return View();
        } 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
