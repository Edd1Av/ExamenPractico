using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services.Interfaces
{
    public interface IFacturaService
    {
        public abstract Response CreateFactura(FacturaModel product);
        public abstract Response DeleteFactura(int id);
        public abstract List<Factura> GetFacturas();
    }
}
