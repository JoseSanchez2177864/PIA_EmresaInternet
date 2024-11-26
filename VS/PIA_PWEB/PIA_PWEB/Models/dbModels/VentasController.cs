using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PIA_PWEB.Models.dbModels;
using Microsoft.EntityFrameworkCore;

public class VentasController : Controller
{
    private readonly PiaInternetContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public VentasController(PiaInternetContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Ventas
    public async Task<IActionResult> Index()
    {
        var piaInternetContext = _context.Ventas.Include(v => v.IdUsuarioNavigation);
        return View(await piaInternetContext.ToListAsync());
    }

    // POST: Ventas/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int paqueteId)
    {
        // Obtener el usuario autenticado
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized(); // Si el usuario no está autenticado
        }

        // Obtener el paquete seleccionado por el usuario
        var paquete = await _context.Paquetes.FindAsync(paqueteId);
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

        // Agregar la venta al contexto y guardar los cambios
        _context.Ventas.Add(nuevaVenta);
        await _context.SaveChangesAsync();

        // Después de guardar, puedes redirigir a la lista de ventas o a donde necesites
        return RedirectToAction(nameof(Index));
    }

    // Métodos restantes (Edit, Delete, etc.) van aquí...
}

