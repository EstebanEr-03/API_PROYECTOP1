using BochaAPI.Data;
using BochaAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace BochaAPI.Repositorios
{
    public class SQLCategoriaRepositorio : ICategoriaRepositorio //NO OLVIDARSE QUE SE DEBE INYECTAR EL REPOSITORIO EN PROGRAM
    {
        private readonly BochaDbContext dbContext;

        public SQLCategoriaRepositorio(BochaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<Categoria> CrearCategoriaAsync(Categoria categoria)
        {
            await dbContext.Categoria.AddAsync(categoria);
            await dbContext.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria?> DeleteAsync(Guid id)
        {
            var categoriaBuscar = await dbContext.Categoria.FirstOrDefaultAsync(x => x.IdCategoria == id);
            if (categoriaBuscar == null)
            {
                return null;
            }
            dbContext.Categoria.Remove(categoriaBuscar);
            await dbContext.SaveChangesAsync();
            return categoriaBuscar;
        }

        public async Task<List<Categoria>> GetAllAsync(string? filterOn = null, string? filtrerQuery = null, string? sortBy = null, bool isAscending = true)
        {
            var walks = dbContext.Categoria.AsQueryable();


            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filtrerQuery) == false)
            {
                if (filterOn.Equals("Nombre", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Nombre.Contains(filtrerQuery));


                }
            }
            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Nombre", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Nombre) : walks.OrderByDescending(x => x.Nombre);
                }
            }


            return await walks.ToListAsync();

        }

        public async Task<Categoria?> GetByIdAsync(Guid id)
        {
            var categoriaBuscar = await dbContext.Categoria.FirstOrDefaultAsync(x => x.IdCategoria == id);
            if (categoriaBuscar == null)
            {
                return null;
            }
            return categoriaBuscar;
        }

        public async Task<Categoria?> PutAsync(Guid id, Categoria categoria)
        {
            var categoriaBuscar = await dbContext.Categoria.FirstOrDefaultAsync(x => x.IdCategoria == id);
            if (categoriaBuscar == null)
            {
                return null;
            }
            categoriaBuscar.CategoriaImageURL = categoria.CategoriaImageURL;
            categoriaBuscar.IdCategoria= categoria.IdCategoria;
            categoriaBuscar.Code = categoria.Code;
            categoriaBuscar.Nombre = categoria.Nombre;

            await dbContext.SaveChangesAsync();
            return categoriaBuscar;


        }
    }


}
