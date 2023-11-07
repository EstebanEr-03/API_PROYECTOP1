using BochaAPI.Data;
using BochaAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace BochaAPI.Repositorios
{
    public class SQLMarcaRepositorio : IMarcaRepositorio //NO OLVIDARSE QUE SE DEBE INYECTAR EL REPOSITORIO EN PROGRAM
    {
        private readonly BochaDbContext dbContext;

        public SQLMarcaRepositorio(BochaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<Marca> CrearMarcaAsync(Marca marca)
        {
            await dbContext.Marca.AddAsync(marca);
            await dbContext.SaveChangesAsync();
            return marca;
        }


        public async Task<Marca?> DeleteAsync(Guid id)
        {
            var marcaBuscar = await dbContext.Marca.FirstOrDefaultAsync(x => x.IdMarca == id);
            if (marcaBuscar == null)
            {
                return null;
            }
            dbContext.Marca.Remove(marcaBuscar);
            await dbContext.SaveChangesAsync();
            return marcaBuscar;
        }

        public async Task<List<Marca>> GetAllAsync(string? filterOn = null, string? filtrerQuery = null, string? sortBy = null, bool isAscending = true)
        {
            var walks = dbContext.Marca.AsQueryable();


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

        public async Task<Marca?> GetByIdAsync(Guid id)
        {
            var marcaBuscar = await dbContext.Marca.FirstOrDefaultAsync(x => x.IdMarca == id);
            if (marcaBuscar == null)
            {
                return null;
            }
            return marcaBuscar;
        }

        public async Task<Marca?> PutAsync(Guid id, Marca marca)
        {
            var marcaBuscar = await dbContext.Marca.FirstOrDefaultAsync(x => x.IdMarca == id);
            if (marcaBuscar == null)
            {
                return null;
            }
            marcaBuscar.IdMarca= marca.IdMarca;
            marcaBuscar.Nombre = marca.Nombre;

            await dbContext.SaveChangesAsync();
            return marcaBuscar;


        }
    }


}
