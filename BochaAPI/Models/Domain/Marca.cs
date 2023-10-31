using System.ComponentModel.DataAnnotations;

namespace BochaAPI.Domain
{
    public class Marca
    {
        [Key]
        public Guid IdMarca { get; set; }
        public string Nombre { get; set; }

    }
}
