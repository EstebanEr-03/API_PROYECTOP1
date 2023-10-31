namespace BochaAPI.Models.DTO
{
    public class CategoriaDTO
    {
        public Guid IdCategoria { get; set; }
        public string Code { get; set; }
        public string Nombre { get; set; }

        public string? CategoriaImageURL { get; set; }

    }
}
