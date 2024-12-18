﻿using System;
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
    public class PaquetesController : Controller
    {
        private readonly PiaInternetContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public PaquetesController(UserManager<ApplicationUser> userManager, PiaInternetContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Paquetes
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                ViewBag.UserRole = roles.FirstOrDefault(); // Pasamos el rol al ViewBag
            }
            return View(await _context.Paquetes.ToListAsync());
        }
        public async Task<IActionResult> Index1()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                ViewBag.UserRole = roles.FirstOrDefault(); // Pasamos el rol al ViewBag
            }
            return View(await _context.Paquetes.ToListAsync());
        }

        // GET: Paquetes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquetes
                .FirstOrDefaultAsync(m => m.IdPaquete == id);
            if (paquete == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                ViewBag.UserRole = roles.FirstOrDefault(); // Pasamos el rol al ViewBag
            }
            return View(paquete);
        }

        // GET: Paquetes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paquetes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPaquete,NombrePaquete,Precio,Imagen,Descripcion")] Paquete paquete)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paquete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paquete);
        }

        // GET: Paquetes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquetes.FindAsync(id);
            if (paquete == null)
            {
                return NotFound();
            }
            return View(paquete);
        }

        // POST: Paquetes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPaquete,NombrePaquete,Precio,Imagen,Descripcion")] Paquete paquete)
        {
            if (id != paquete.IdPaquete)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paquete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaqueteExists(paquete.IdPaquete))
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
            return View(paquete);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarVenta(int idPaquete)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(); // Si el usuario no está autenticado
            }

            // Obtener el paquete seleccionado por el usuario
            var paquete = await _context.Paquetes.FindAsync(idPaquete);  // Usamos 'idPaquete' en vez de 'paqueteId'
            if (paquete == null)
            {
                return NotFound(); // Si no se encuentra el paquete
            }

            // Crear una nueva venta asociada con el usuario y el paquete
            var nuevaVenta = new Venta
            {
                IdUsuario = user.Id, // El usuario autenticado
                Fecha = DateTime.Now, // Fecha actual
                TotalVenta = paquete.Precio // Precio del paquete
            };

            // No asignamos IdVenta manualmente; EF Core lo generará automáticamente

            // Agregar la venta al contexto y guardar los cambios
            _context.Ventas.Add(nuevaVenta);
            await _context.SaveChangesAsync();

            // Después de guardar, puedes redirigir a la lista de ventas o a donde necesites
            return RedirectToAction(nameof(Index));
        }


        // GET: Paquetes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquetes
                .FirstOrDefaultAsync(m => m.IdPaquete == id);
            if (paquete == null)
            {
                return NotFound();
            }

            return View(paquete);
        }

        // POST: Paquetes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paquete = await _context.Paquetes.FindAsync(id);
            if (paquete != null)
            {
                _context.Paquetes.Remove(paquete);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaqueteExists(int id)
        {
            return _context.Paquetes.Any(e => e.IdPaquete == id);
        }
    }
}
