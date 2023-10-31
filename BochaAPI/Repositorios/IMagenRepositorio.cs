using BochaAPI.Models.Domain;

namespace BochaAPI.Repositorios
{
    public interface IMagenRepositorio
    {
        Task<Imagen> SubirImagen(Imagen imagen);
    }
}
