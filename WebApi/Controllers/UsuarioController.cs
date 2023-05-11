using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("clientes")]
        public IActionResult GetUsuarios()
        {
            List<Usuario> usuarios = _usuarioService.GetClientes();
            return Ok(usuarios);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostUsuario(UsuarioModel usuario)
        {

            Response resp = await _usuarioService.CreateUsuario(usuario);
            return Ok(resp);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUsuario(Delete usuario)
        {
            Response resp = await _usuarioService.DeleteUsuario(usuario.Id);
            return Ok(resp);
        }
    }
}
