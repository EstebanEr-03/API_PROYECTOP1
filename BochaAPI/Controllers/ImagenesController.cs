using BochaAPI.Models.Domain;
using BochaAPI.Models.DTO;
using BochaAPI.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BochaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenesController : ControllerBase
    {
        private readonly IMagenRepositorio magenRepositorio;

        public ImagenesController(IMagenRepositorio magenRepositorio)
        {
            this.magenRepositorio = magenRepositorio;
        }
        //Post : /api/Imagenes/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> SubirImg([FromForm] ImageUploadRequestDTO request)
        {

            ValidarSubirImagen(request);
            if (ModelState.IsValid ) 
            {
                //Convert DTO a Domain Model
                var ImagenDomainModel = new Imagen
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescricion = request.FileDescricion,
                };




                //Si es valido se sube la imagen
                await magenRepositorio.SubirImagen(ImagenDomainModel);
                return Ok(ImagenDomainModel);

            }
            //Si no es valido error
            return BadRequest(ModelState);
        }

        private void ValidarSubirImagen(ImageUploadRequestDTO request)
        {
            var extensionesPermitidas = new String[] { ".jpg", ".jpeg", ".png", };
            if (extensionesPermitidas.Contains(Path.GetExtension(request.FileName)))
            {
                ModelState.AddModelError("file", "Extension no permitida");
            }
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "Imagen demasiado grande, porfavor subir una <10mb");
            }
        }
    }
}
