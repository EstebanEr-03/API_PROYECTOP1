using BochaStore.UI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BochaStore.UI.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ProductoController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {

            List<ProductoDTO> respuesta = new List<ProductoDTO>();

            try
            {
                //Obtine todos los productos de la API
                var cliente = httpClientFactory.CreateClient(); //Crea un nuevo cliente Http


                var httpRespuesta = await cliente.GetAsync("https://localhost:7042/api/region"); //Responde un http

                httpRespuesta.EnsureSuccessStatusCode(); //Si no es verdadero devuelve falso y va al catch

                respuesta.AddRange(await httpRespuesta.Content.ReadFromJsonAsync<IEnumerable<ProductoDTO>>());
            }
            catch (Exception ex)
            {

                throw;
            }


            return View(respuesta);
        }
    }
}
