using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;

namespace PIA_PWEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OpinionesAdminController : Controller
    {
        private readonly PiaInternetContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public OpinionesAdminController(UserManager<ApplicationUser> userManager, PiaInternetContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: OpinionesAdmin
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                ViewBag.UserRole = roles.FirstOrDefault(); // Pasamos el rol al ViewBag
            }
            return View(await _context.Opiniones.ToListAsync());
        }

        // GET: OpinionesAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opinione = await _context.Opiniones
                .Include(o => o.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdOpinion == id);
            if (opinione == null)
            {
                return NotFound();
            }

            return View(opinione);
        }

        // GET: OpinionesAdmin/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: OpinionesAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOpinion,IdUsuario,Opinion,Fecha,Anonimo")] Opinione opinione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opinione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", opinione.IdUsuario);
            return View(opinione);
        }

        // GET: OpinionesAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opinione = await _context.Opiniones.FindAsync(id);
            if (opinione == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", opinione.IdUsuario);
            return View(opinione);
        }

        // POST: OpinionesAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOpinion,IdUsuario,Opinion,Fecha,Anonimo")] Opinione opinione)
        {
            if (id != opinione.IdOpinion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opinione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpinioneExists(opinione.IdOpinion))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", opinione.IdUsuario);
            return View(opinione);
        }

        // GET: OpinionesAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opinione = await _context.Opiniones
                .Include(o => o.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdOpinion == id);
            if (opinione == null)
            {
                return NotFound();
            }

            return View(opinione);
        }

        // POST: OpinionesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opinione = await _context.Opiniones.FindAsync(id);
            if (opinione != null)
            {
                _context.Opiniones.Remove(opinione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpinioneExists(int id)
        {
            return _context.Opiniones.Any(e => e.IdOpinion == id);
        }
    }
}
