namespace GestApp.Models.DTOs
{
    public class UsuarioCreateDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = "User";
    }
}
