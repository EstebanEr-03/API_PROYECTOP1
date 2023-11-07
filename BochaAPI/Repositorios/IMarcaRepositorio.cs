    using BochaAPI.Domain;

namespace BochaAPI.Repositorios
{
    //Imporatante implementar que es una interface
    public interface IMarcaRepositorio
    {
        Task<Marca> CrearMarcaAsync(Marca caminata);
        Task<List<Marca>> GetAllAsync(string? filterOn = null, string? filtrerQuery = null, string? sortBy = null, bool isAscending = true);
        Task<Marca?> GetByIdAsync(Guid id);
        Task<Marca?> PutAsync(Guid id, Marca caminata);
        Task<Marca?> DeleteAsync(Guid id);
    }
}