using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Data
{
    public class VentaDbContext : IdentityDbContext<ApplicationUser>
    {
        public VentaDbContext(DbContextOptions<VentaDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            modelBuilder.Entity<Usuario>().HasIndex(x => x.Correo_Electronico).IsUnique();
            modelBuilder.Entity<Factura>().HasIndex(x => x.Folio).IsUnique();

            var hasher = new PasswordHasher<IdentityUser>();

            var admin = new ApplicationUser()
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                //IdUsuario =
                Email = "admin@admin.com",
                EmailConfirmed = true,
                NormalizedEmail = "ADMIN@ADMIN.COM",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                PasswordHash = hasher.HashPassword(null, "Pa$word1")
            };

            var ger = new ApplicationUser()
            {
                Id = "8e445865-a68d-4543-a6c6-9443d048cdb9",
                //IdUsuario =
                Email = "ger@ger.com",
                EmailConfirmed = true,
                NormalizedEmail = "GER@GER.COM",
                UserName = "Ger",
                NormalizedUserName = "GER",
                PasswordHash = hasher.HashPassword(null, "Pa$word1")
            };

            var rolAdministrador = new IdentityRole()
            {
                Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                Name = "ADMINISTRADOR",
                NormalizedName = "ADMINISTRADOR"
            };

            var rolCliente = new IdentityRole()
            {
                Id = "3c6e284e-4b1e-557f-97af-594d67fd8321",
                Name = "CLIENTE",
                NormalizedName = "CLIENTE"
            };

            var rolGerente = new IdentityRole()
            {
                Id = "dad25a92-d54e-421a-b070-15975d5794eb",
                Name = "GERENTE",
                NormalizedName = "GERENTE"
            };

            var user1 = new IdentityUserRole<string>()
            {
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
            };

            var user2 = new IdentityUserRole<string>()
            {
                UserId = "8e445865-a68d-4543-a6c6-9443d048cdb9",
                RoleId = "dad25a92-d54e-421a-b070-15975d5794eb"
            };
            //modelBuilder.Entity<Colaborador>().HasData(userData);
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole[] { rolAdministrador, rolCliente, rolGerente });
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser[] { admin, ger});
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>[] { user1, user2 });
        }


        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Factura> Facturas { get; set; }

    }
}
