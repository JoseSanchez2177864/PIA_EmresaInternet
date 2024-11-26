using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;

namespace PIA_PWEB.Controllers
{
    public class OpinionesController : Controller
    {
        private readonly PiaInternetContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OpinionesController(UserManager<ApplicationUser> userManager, PiaInternetContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Opiniones
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var opiniones = await _context.Opiniones
                .Include(o => o.IdUsuarioNavigation) // Incluir la relación de usuario
                .OrderByDescending(o => o.Fecha) // Ordenar por fecha descendente
                .ToListAsync();

            return View(opiniones); // Enviar las opiniones a la vista
        }

        // POST: Opiniones
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Opinion,Anonimo")] Opinione opinione)
        {
            if (ModelState.IsValid)
            {
                // Obtener el usuario autenticado
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    // Asignar valores a la nueva opinión
                    opinione.IdUsuario = user.Id;
                    opinione.Fecha = DateTime.Now; // Fecha actual

                    // Agregar la opinión a la base de datos
                    _context.Add(opinione);
                    await _context.SaveChangesAsync(); // Guardar los cambios

                    // Redirigir a Index para mostrar la lista actualizada
                    return RedirectToAction(nameof(Index));
                }

            }

            // Si falla, recargar la lista de opiniones
            var opiniones = await _context.Opiniones
                .Include(o => o.IdUsuarioNavigation)
                .OrderByDescending(o => o.Fecha)
                .ToListAsync();

            return View(opiniones); // Regresar a la vista con los datos actuales
        }
    }
}
