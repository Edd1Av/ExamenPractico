using WebApi.Models;

namespace WebApi.Services.Interfaces
{
    public interface IIdentityService
    {
        public abstract Task<LocalStorage> Login(LoginUsuario credenciales);
    }
}
