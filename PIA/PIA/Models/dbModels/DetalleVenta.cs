using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA.Models.dbModels;

[PrimaryKey("IdVenta", "IdPaquete")]
public partial class DetalleVenta
{
    [Key]
    [Column("idVenta")]
    public int IdVenta { get; set; }

    [Key]
    [Column("idPaquete")]
    public int IdPaquete { get; set; }

    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Precio { get; set; }

    [ForeignKey("IdPaquete")]
    [InverseProperty("DetalleVenta")]
    public virtual Paquete IdPaqueteNavigation { get; set; } = null!;

    [ForeignKey("IdVenta")]
    [InverseProperty("DetalleVenta")]
    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
