using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;
using System.Linq;

namespace PIA_PWEB.Controllers
{
    public class RolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            // Obtén todos los roles
            var allRoles = _roleManager.Roles.ToList();

            // Obtén todos los usuarios junto con sus roles
            var users = await _userManager.Users.ToListAsync();

            var userRoles = new List<object>();
            foreach (var user in users)
            {
                var userAssignedRoles = await _userManager.GetRolesAsync(user); // Renombramos aquí
                userRoles.Add(new
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = userAssignedRoles
                });
            }

            // Prepara el modelo de vista para pasar los datos
            var viewModel = new
            {
                Roles = allRoles, // Usamos allRoles en lugar de roles
                UserRoles = userRoles
            };
            var userAC = await _userManager.GetUserAsync(User);
            if (userAC != null)
            {
                var roles = await _userManager.GetRolesAsync(userAC);
                ViewBag.UserRole = roles.FirstOrDefault(); // Pasamos el rol al ViewBag
            }
            return View(viewModel);
        }
    }
}


