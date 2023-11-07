using BochaAPI.Domain;

namespace BochaAPI.Repositorios
{
    //Imporatante implementar que es una interface
    public interface IProductoRepositorio
    {
        Task<Producto> CrearProductoAsync(Producto caminata);
        Task<List<Producto>> GetAllAsync(string? filterOn=null,string? filtrerQuery=null,string? sortBy=null,bool isAscending=true);
        Task<Producto?> GetByIdAsync(Guid id);
        Task<Producto?> PutAsync(Guid id, Producto caminata);
        Task<Producto?> DeleteAsync(Guid id);
    }
}
