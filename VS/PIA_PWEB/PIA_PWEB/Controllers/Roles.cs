using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PIA_PWEB.Models.dbModels;


namespace PIA_PWEB.Controllers
{
    public class Roles(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return BadRequest("El rol no existe.");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return BadRequest("No se pudo asignar el rol.");
        }

        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return BadRequest("No se pudo remover el rol.");
        }
    }
}
