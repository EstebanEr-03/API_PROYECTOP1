using AutoMapper;
using BochaAPI.Domain;
using BochaAPI.Models.DTO;

namespace BochaAPI.Mappings
{
    public class PerfilesAutoMappers:Profile
    {
        public PerfilesAutoMappers()
        {
            //necesita dos parametros, source y destino
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();

            CreateMap<AddCategoriaRequestDTO, Categoria>().ReverseMap();//AddRegionRequestDto source-----Region(DomainModel)destino
            
            //MapearCaminatas

            CreateMap<AddProductoRequestDTO, Producto>().ReverseMap();//AddRegionRequestDto source-----Region(DomainModel)destino
            CreateMap<Producto, ProductoDTO>().ReverseMap();

            CreateMap<Marca, MarcaDTO>().ReverseMap();
            CreateMap<AddMarcaRequestDTO, Marca>().ReverseMap();
        }
    }
}
