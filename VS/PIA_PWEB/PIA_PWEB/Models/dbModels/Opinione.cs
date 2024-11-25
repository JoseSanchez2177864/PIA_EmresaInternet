using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PIA_PWEB.Models.dbModels;
[Route("api/[controller]")]
[ApiController]
public partial class Opinione
{
    [Key]
    [Column("idOpinion")]
    public int IdOpinion { get; set; }

    [Column("idUsuario")]
    public int IdUsuario { get; set; }

    [Column(TypeName = "text")]
    public string Opinion { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    public bool Anonimo { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("Opiniones")]
    public virtual ApplicationUser IdUsuarioNavigation { get; set; } = null!;
}
