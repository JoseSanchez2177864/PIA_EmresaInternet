using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PIA_PWEB.Models.dbModels
{
    public class ApplicationUser : IdentityUser<int>
    {

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Opinione> Opiniones { get; set; } = new List<Opinione>();

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
    }
}
