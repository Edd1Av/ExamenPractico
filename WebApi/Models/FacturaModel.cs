using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Models
{
    public class FacturaModel
    {
        public int Id { get; set; }
        public int Id_usuario { get; set; }
        public string? Folio { get; set; }
        public float Saldo { get; set; }
        public DateTime Fecha_facturacion { get; set; }
        public DateTime Fecha_creacion { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
