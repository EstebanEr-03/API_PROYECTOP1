namespace BochaStore.UI.Models.DTO
{
    public class ProductoDTO
    { 
        public Guid IdRegion { get; set; }
        public string Code { get; set; }
        public string Nombre { get; set; }

        public string? RegionImageURL { get; set; }
    }
}
