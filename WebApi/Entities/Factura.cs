using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Factura
    {
        public int Id { get; set; }
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        [StringLength(10)]
        public string? Folio { get; set; }
        [Required]
        public float Saldo { get; set; }
        [Required]
        public DateTime Fecha_Facturacion { get; set; }
        [Required]
        public DateTime Fecha_Creacion { get; set; }
        [ForeignKey("Id_Usuario")]
        public Usuario Usuario { get; set;}
    }
}
