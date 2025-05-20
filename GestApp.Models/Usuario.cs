namespace GestApp.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Pass { get; set; } = string.Empty;
        public string Rol { get; set; } = "User"; // Default a User

        public DateTime FechaAlta { get; set; } = DateTime.Now;
    }
}
