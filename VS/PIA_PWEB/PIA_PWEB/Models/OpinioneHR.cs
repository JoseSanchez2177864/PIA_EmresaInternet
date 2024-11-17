using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PIA_PWEB.Models
{
    public class OpinioneHR
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
    }
}
