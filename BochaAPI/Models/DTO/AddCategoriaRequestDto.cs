using BochaAPI.Domain;

namespace BochaAPI.Models.DTO
{
    public class AddCategoriaRequestDTO
    {
        public string Code { get; set; }
        public string Nombre { get; set; }

        public string? CategoriaImageURL { get; set; }


    }
}
