namespace GestApp.Models
{
    public class UsuarioRegisterDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Pass { get; set; } = string.Empty;
        public string Rol { get; set; } = "User";
    }
}
