using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int Edad { get; set; }
        public string? Correo_electronico { get; set; }
        
        public int Tipo_usuario { get; set; }
      
        public List<Factura> Facturas { get; set; }
    }

    public class Delete
    {
        public int Id { get; set; }
        public string? User { get; set; }
    }
}
