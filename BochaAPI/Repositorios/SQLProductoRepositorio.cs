using BochaAPI.Data;
using BochaAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace BochaAPI.Repositorios
{
    public class SQLProductoRepositorio : IProductoRepositorio //NO OLVIDARSE QUE SE DEBE INYECTAR EL REPOSITORIO EN PROGRAM
    {
        private readonly BochaDbContext dbContext;

        public SQLProductoRepositorio(BochaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<Producto> CrearProductoAsync(Producto producto)
        {
           await dbContext.Producto.AddAsync(producto);
           await dbContext.SaveChangesAsync();
           return producto;
        }

        public async Task<Producto?> DeleteAsync(Guid id)
        {
            var productoBuscar = await dbContext.Producto.FirstOrDefaultAsync(x => x.IdProducto == id);
            if (productoBuscar == null)
            {
                return null;
            }
            dbContext.Producto.Remove(productoBuscar);
            await dbContext.SaveChangesAsync();
            return productoBuscar;
        }

        public async Task<List<Producto>> GetAllAsync(string? filterOn = null, string? filtrerQuery = null, string? sortBy = null, bool isAscending = true)
        {
            var walks = dbContext.Producto.AsQueryable();


            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filtrerQuery) ==false)
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
                else if (sortBy.Equals("Cantidad", StringComparison.OrdinalIgnoreCase))
                { 
                    walks = isAscending ? walks.OrderBy(x => x.Cantidad) : walks.OrderByDescending(x => x.Cantidad);
                }
            }


            return await walks.ToListAsync();
            
        }

        public async Task<Producto?> GetByIdAsync(Guid id)
        {
            var productoBuscar = await dbContext.Producto.FirstOrDefaultAsync(x => x.IdProducto == id);
            if (productoBuscar == null)
            {
                return null;
            }
            return productoBuscar;
        }

        public async Task<Producto?> PutAsync(Guid id, Producto producto)
        {
            var productoBuscar = await dbContext.Producto.FirstOrDefaultAsync(x => x.IdProducto == id);
            if (productoBuscar == null)
            {
                return null;
            }
            productoBuscar.Descripcion = producto.Descripcion;
            productoBuscar.ImagenProductoURL = producto.ImagenProductoURL;
            productoBuscar.IdMarca = producto.IdMarca;
            productoBuscar.Cantidad = producto.Cantidad;
            productoBuscar.IdCategoria = producto.IdCategoria;
            productoBuscar.Nombre   = producto.Nombre;
            productoBuscar.Precio = producto.Precio;

            await dbContext.SaveChangesAsync();
            return productoBuscar;


        }
    }


}
