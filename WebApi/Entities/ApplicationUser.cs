using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int? IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }



    }
}
