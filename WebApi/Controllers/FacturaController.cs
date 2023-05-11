using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public FacturaController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetFacturas()
        {
            List<Factura> facturas = _facturaService.GetFacturas();
            return Ok(facturas);

        }

        [HttpPost]
        [Authorize]
        public IActionResult PostFactura(FacturaModel factura)
        {

            Response resp = _facturaService.CreateFactura(factura);
            return Ok(resp);
        }

     
        [HttpPost]
        [Route("delete")]
        [Authorize]
        public IActionResult DeleteFactura(Delete factura)
        {
            Response resp = _facturaService.DeleteFactura(factura.Id);
            return Ok(resp);
        }
    }
}
