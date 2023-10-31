namespace BochaAPI.Models.DTO
{
    public class UpdateCategoriaRequestDTO
    {
        public string Code { get; set; }
        public string Nombre { get; set; }

        public string? CategoriaImageURL { get; set; }
    }
}
