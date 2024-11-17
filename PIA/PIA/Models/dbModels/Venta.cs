using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA.Models.dbModels;

public partial class Venta
{
    [Key]
    [Column("idVenta")]
    public int IdVenta { get; set; }

    [Column("idUsuario")]
    public int IdUsuario { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalVenta { get; set; }

    [InverseProperty("IdVentaNavigation")]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("Venta")]
    public virtual ApplicationUser IdUsuarioNavigation { get; set; } = null!;
}
