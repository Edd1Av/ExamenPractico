using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Data;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly VentaDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string SecretKey;
        public IdentityService(UserManager<ApplicationUser> userManager, VentaDbContext context, IConfiguration config)
        {
            SecretKey = config.GetSection("Settings").GetSection("SecretKey").ToString();
            _context = context;
            _userManager = userManager;
        }

        public async Task<LocalStorage> Login(LoginUsuario credenciales)
        {
            var user = await _userManager.FindByEmailAsync(credenciales.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, credenciales.Password))
            {
                return new LocalStorage { Success = false , Message="Datos incorrectos"};
            }

            try
            {
                var keyBytes = Encoding.ASCII.GetBytes(SecretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Email));
                var expiration = DateTime.Now.AddDays(1);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = expiration,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                string Token = tokenHandler.WriteToken(tokenConfig);
                var rol = _userManager.GetRolesAsync(user);

                LocalStorage localS = new LocalStorage { IdUsuario = (int)(user.IdUsuario != null ? user.IdUsuario : 0), Correo = user.Email, Token = Token, Expiration = expiration, Rol = rol.Result.First(), RolId = user.Usuario!=null ? user.Usuario.Tipo_Usuario: 0, Success = true };
                return localS;
            }
            catch(Exception ex)
            {
                return new LocalStorage { Success = false, Message = $"{ex.Message}" };
            }
        }
    }
}
