using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Data;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Models.Enum;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly VentaDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string SecretKey;
        private IConfiguration _config;
        public IdentityService(UserManager<ApplicationUser> userManager, VentaDbContext context, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
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
                var tokenHandler = new JwtSecurityTokenHandler();
                var expiresDate = DateTime.UtcNow.AddDays(1);
                var llave = Encoding.ASCII.GetBytes(_config["Settings:SecretKey"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Email, user.Email)
                        }
                        ),
                    Expires = expiresDate,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256)

                };
                var tokenH = tokenHandler.CreateToken(tokenDescriptor);


                //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Settings:SecretKey"]));


                //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                //var claims = new ClaimsIdentity();
                //claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Email));

                //var expiration = DateTime.Now.AddMinutes(120);
                //var token = new JwtSecurityToken(
                //  null,
                //  expires: expiration,
                //  signingCredentials: credentials);

                var Token =  tokenHandler.WriteToken(tokenH);

                var rol = _userManager.GetRolesAsync(user);

                var x = Enum.TryParse(rol.Result.First(), out ETipoUsuario myStatus);
                
                LocalStorage localS = new LocalStorage { IdUsuario = (int)(user.IdUsuario != null ? user.IdUsuario : 0), Correo = user.Email, Token = Token, Expiration = expiresDate, Rol = rol.Result.First(), RolId = (int)myStatus, Success = true };
                return localS;
            }
            catch(Exception ex)
            {
                return new LocalStorage { Success = false, Message = $"{ex.Message}" };
            }
        }
    }
}
