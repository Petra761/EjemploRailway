using System.ComponentModel.DataAnnotations;

namespace EjemploRailway.Entities
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }
        public string CI { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
