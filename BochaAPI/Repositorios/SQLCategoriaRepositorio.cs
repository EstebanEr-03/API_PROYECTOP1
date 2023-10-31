using BochaAPI.Data;
using BochaAPI.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BochaAPI.Repositorios
{
    public class SQLCategoriaRepositorio : ICategoriaRepositorio
    {

        //Inyectar DbContextClass
        private readonly BochaDbContext dbContext;

        public SQLCategoriaRepositorio(BochaDbContext dbContext)
        {
            this.dbContext=dbContext;
        }

        public async Task<Categoria> CreateAsync(Categoria CategoriaDomainModel)
        {
             await dbContext.Categoria.AddAsync(CategoriaDomainModel);
             await dbContext.SaveChangesAsync();
            return CategoriaDomainModel;
        }

        public async Task<Categoria?> DeleteAsync(Guid id)
        {
           var CategoriaBuscar= await dbContext.Categoria.FirstOrDefaultAsync(x => x.IdCategoria == id);
            if (CategoriaBuscar == null) 
            {
                return null;
            }
             dbContext.Categoria.Remove(CategoriaBuscar);
            await dbContext.SaveChangesAsync();
            return CategoriaBuscar;
        }



        //Implementar interfaz
        public async Task<List<Categoria>> GetAllAsync()
        {
            return await dbContext.Categoria.ToListAsync();
        }

        public async Task<Categoria?> GetByIdAsync(Guid id)
        {
            return await dbContext.Categoria.FirstOrDefaultAsync(x => x.IdCategoria == id);
        }

        public async Task<Categoria?> UpdateAsync(Guid id, Categoria CategoriaDomainModel)
        {
            var CategoriaEncontrada = await dbContext.Categoria.FirstOrDefaultAsync(x => x.IdCategoria == id);
            if (CategoriaEncontrada == null)
            {
                return null;
            }
            CategoriaEncontrada.Nombre = CategoriaDomainModel.Nombre;
            CategoriaEncontrada.Code = CategoriaDomainModel.Code;
            CategoriaEncontrada.CategoriaImageURL = CategoriaDomainModel.CategoriaImageURL;

            await dbContext.SaveChangesAsync();
            return CategoriaEncontrada;
        }
    }
}
