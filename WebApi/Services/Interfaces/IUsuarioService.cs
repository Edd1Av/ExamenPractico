using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services.Interfaces
{
    public interface IUsuarioService
    {
        public abstract Task<Response> CreateUsuario(UsuarioModel product);
        public abstract Task<Response> DeleteUsuario(int id);
        public abstract List<Usuario> GetClientes();
    }
}
