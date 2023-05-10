using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Models.Enum;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class UsuarioService : IUsuarioService 
    {
        private readonly VentaDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioService(VentaDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Response> CreateUsuario(UsuarioModel usuarioM)
        {
            var email = await _userManager.FindByEmailAsync(usuarioM.Correo_electronico.Trim());

            if (email != null)
            {
                return new Response { Success = false, Content = $"Ya existe un usuario con ese Email" };
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    ETipoUsuario tipoUsuario = (ETipoUsuario)usuarioM.Tipo_usuario;
                    Usuario usuarioEntity = new()
                    {
                        Nombre = usuarioM.Nombre.Trim(),
                        Apellido = usuarioM.Apellido.Trim(),
                        Edad = usuarioM.Edad,
                        Correo_Electronico = usuarioM.Correo_electronico.Trim(),
                        Tipo_Usuario = (int)tipoUsuario
                    };

                    _context.Usuarios.Add(usuarioEntity);


                    ApplicationUser user = new ApplicationUser();
                    user.Email = usuarioEntity.Correo_Electronico;
                    user.Usuario = usuarioEntity;
                    user.EmailConfirmed = true;
                    user.UserName = usuarioEntity.Correo_Electronico;

                    string password = "Pa$word1"; //Password.GeneratePassword(_userManager);

                    var createUser = await _userManager.CreateAsync(user, password);

                    if (createUser.Succeeded)
                    {
                        string rol = tipoUsuario.ToString();
                        var asignRol = await _userManager.AddToRoleAsync(user, rol);
                        if (asignRol.Succeeded){
                        }
                        else
                        {
                            transaction.Rollback();
                            return new Response { Success = false, Content = $"No se pudo asignar un rol" };
                        }
                    }
                    else
                    {
                        transaction.Rollback();
                        return new Response { Success = false, Content = $"No se pudo crear el usuario" };
                    }
                    _context.SaveChanges();
                    transaction.Commit();
                }catch(Exception ex)
                {
                    transaction.Rollback();
                    return new Response { Success = false, Content = $"{ex.Message}"};
                }
             }

            return new Response { Success= true , Content="Usuario registrado"};
        }

        public async Task<Response> DeleteUsuario(int id)
        {
            Usuario usuarioEntity = _context.Usuarios.Include(x => x.IdentityUser).Where(x => x.Id == id).FirstOrDefault(); 
            if(usuarioEntity == null)
            {
                return new Response { Success = false, Content = "El usuario no existe" };
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var IUser = await _userManager.FindByEmailAsync(usuarioEntity.IdentityUser.Email);
                    var Delete = await _userManager.DeleteAsync(IUser);

                    if (Delete.Succeeded)
                    {
                        _context.Usuarios.Remove(usuarioEntity);
                    }
                    else
                    {
                        transaction.Rollback();
                        return new Response { Success = false, Content = "Error al eliminar el usuario" };
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch( Exception ex)
                {
                    transaction.Rollback();
                    return new Response { Success = false, Content = $"{ex.Message}" };
                }
            }
            return new Response { Success = true, Content = "Usuario Eliminado" };
        }

 
        public List<Usuario> GetClientes()
        {
            return _context.Usuarios.Include(x=>x.Facturas).Where(x=>x.Tipo_Usuario==(int)ETipoUsuario.CLIENTE).ToList();
        }
    }
}
