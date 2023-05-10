using System.ComponentModel.DataAnnotations;
using WebApi.Models.Enum;

namespace WebApi.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string? Apellido { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        [StringLength(100)]
        public string? Correo_Electronico { get; set; }
        [Required]
        [StringLength(100)]
        public int Tipo_Usuario { get; set; }

        public ApplicationUser IdentityUser { get; set; }
        public List<Factura> Facturas { get; set; }  

    }
}
