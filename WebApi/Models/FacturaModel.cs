using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Models
{
    public class FacturaModel
    {
        public int Id { get; set; }
        public int Id_Usuario { get; set; }
        public string? Folio { get; set; }
        public float Saldo { get; set; }
        public DateTime Fecha_Facturacion { get; set; }
        public DateTime Fecha_Creacion { get; set; }

    }
}
