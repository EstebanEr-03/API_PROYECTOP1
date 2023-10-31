using System.ComponentModel.DataAnnotations;

namespace BochaAPI.Domain
{
    public class Categoria
    {
        [Key]
        public Guid IdCategoria { get; set; }
        public string Code { get; set; }
        public string Nombre { get; set; }

        public string? CategoriaImageURL { get; set; }
    }
}
