using BochaAPI.Domain;

namespace BochaAPI.Repositorios
{
    public interface ICategoriaRepositorio
    {
        //definicion 
       Task<List<Categoria>> GetAllAsync();
       Task<Categoria?> GetByIdAsync(Guid id);//?nulleable region 
       Task<Categoria> CreateAsync(Categoria categoriaDomainModel);
       Task<Categoria?> UpdateAsync(Guid id,Categoria categoriaDomainModel);//?nulleable region  porque el id puede que no se encuentre
       Task<Categoria?> DeleteAsync(Guid id);//?nulleable region  porque el id puede que no se encuentre
    }
}
