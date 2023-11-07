    using BochaAPI.Domain;

namespace BochaAPI.Repositorios
{
    //Imporatante implementar que es una interface
    public interface ICategoriaRepositorio
    {
        Task<Categoria> CrearCategoriaAsync(Categoria caminata);
        Task<List<Categoria>> GetAllAsync(string? filterOn = null, string? filtrerQuery = null, string? sortBy = null, bool isAscending = true);
        Task<Categoria?> GetByIdAsync(Guid id);
        Task<Categoria?> PutAsync(Guid id, Categoria caminata);
        Task<Categoria?> DeleteAsync(Guid id);
    }
}