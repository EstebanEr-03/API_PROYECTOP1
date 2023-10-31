using BochaAPI.Data;
using BochaAPI.Models.Domain;

namespace BochaAPI.Repositorios
{
    public class LocalImagenRepositorio : IMagenRepositorio
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly BochaDbContext bochaDbContext;

        public LocalImagenRepositorio(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,BochaDbContext bochaDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.bochaDbContext = bochaDbContext;
        }
        public async Task<Imagen> SubirImagen(Imagen imagen)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Imagenes",
                $"{imagen.FileName}{imagen.FileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);//esta linea lee el file stream y crea el archivo
            await imagen.File.CopyToAsync(stream);//se agrega a la carpeta img

            //https://localhost... ==== Scheme

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Imagenes/{imagen.FileName}{imagen.FileExtension}";

            imagen.FilePath = urlFilePath;

            //Add imagenes a la base de datos

            await bochaDbContext.Imagenes.AddAsync(imagen);
            await bochaDbContext.SaveChangesAsync();

            return imagen;
        }
    }
}
