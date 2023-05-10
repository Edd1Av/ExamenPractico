namespace WebApi.Models
{
    public class LocalStorage
    {
        public int IdUsuario { get; set; } = 0;
        public string Correo { get; set; } = "";

        public string Token { get; set; } = "";

        public DateTime Expiration { get; set; }

        public string? Rol { get; set; } = "";

        public int RolId { get; set; }

        public bool Success { get; set; } = false;

        public string? Message { get; set; }
    }
}
