using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly VentaDbContext _context;

        public FacturaService(VentaDbContext context)
        {
            _context = context;
        }

        public Response CreateFactura(FacturaModel facturaM)
        {
            Factura facturaEntity = new()
            {
                Id_Usuario = facturaM.Id_Usuario,
                Folio = facturaM.Folio,
                Saldo = facturaM.Saldo,
                Fecha_Facturacion = facturaM.Fecha_Facturacion,
                Fecha_Creacion = DateTime.Now
            };

            _context.Facturas.Add(facturaEntity);
            _context.SaveChanges();
            return new Response { Success = true, Content = "Factura registrada" };
        }

        public Response DeleteFactura(int id)
        {
            Factura facturaEntity = _context.Facturas.Find(id);
            if (facturaEntity != null)
            {
                _context.Facturas.Remove(facturaEntity);
                _context.SaveChanges();
                return new Response { Success = true, Content = "Factura eliminada" };
            }
            else
            {
                return new Response { Success = false, Content = "La factura no existe" };
            }
        }

        public List<Factura> GetFacturas()
        {
            return _context.Facturas.Include(x=>x.Usuario).ToList();

        }
    }
}
