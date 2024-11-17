using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_PWEB.Models.dbModels;

public partial class Paquete
{
    [Key]
    [Column("idPaquete")]
    public int IdPaquete { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NombrePaquete { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Precio { get; set; }

    public byte[]? Imagen { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [InverseProperty("IdPaqueteNavigation")]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();
}
