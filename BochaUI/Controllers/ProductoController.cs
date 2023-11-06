using BochaUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace BochaUI.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public ProductoController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Producto> respuesta = new List<Producto>();
            try
            {
                var cliente = httpClientFactory.CreateClient();
                var httpRespuesta = await cliente.GetAsync("https://localhost:7042/api/Producto");
                httpRespuesta.EnsureSuccessStatusCode();
                respuesta.AddRange(await httpRespuesta.Content.ReadFromJsonAsync<IEnumerable<Producto>>());
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(respuesta);
        }

        [HttpGet]
        public async Task<IActionResult> Resumen()
        {
            List<Producto> respuesta = new List<Producto>();
            try
            {
                var cliente = httpClientFactory.CreateClient();
                var httpRespuesta = await cliente.GetAsync("https://localhost:7042/api/Producto");
                httpRespuesta.EnsureSuccessStatusCode();
                respuesta.AddRange(await httpRespuesta.Content.ReadFromJsonAsync<IEnumerable<Producto>>());
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(respuesta);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid IdProducto)
        {

            var cliente = httpClientFactory.CreateClient();
            var respuesta = await cliente.GetFromJsonAsync<Producto>($"https://localhost:7042/api/Producto/{IdProducto.ToString()}");

            if (respuesta is not null)
            {
                return View(respuesta);
            }
            return View(null);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid IdProducto)
        {

            var cliente = httpClientFactory.CreateClient();
            var respuesta = await cliente.GetFromJsonAsync<Producto>($"https://localhost:7042/api/Producto/{IdProducto.ToString()}");

            if (respuesta is not null)
            {
                return View(respuesta);
            }
            return View(null);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto solicitud)
        {

            var cliente = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7042/api/Producto/"),
                Content = new StringContent(JsonSerializer.Serialize(solicitud), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await cliente.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respuesta = await httpResponseMessage.Content.ReadFromJsonAsync<Producto>();

            if (respuesta is not null)
            {
                return RedirectToAction("Resumen", "Producto");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Producto solicitud)
        {
            var cliente = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7042/api/Producto/{solicitud.IdProducto.ToString()}"),
                Content = new StringContent(JsonSerializer.Serialize(solicitud), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await cliente.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respuesta = await httpResponseMessage.Content.ReadFromJsonAsync<Producto>();

            return RedirectToAction("Resumen", "Producto");

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid IdProducto)
        {
            try
            {
                var cliente = httpClientFactory.CreateClient();
                var httpResponseMessage = await cliente.DeleteAsync($"https://localhost:7042/api/Producto/{IdProducto}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    // La eliminación fue exitosa, puedes redirigir al usuario a la página de resumen o realizar otra acción apropiada.
                    return RedirectToAction("Resumen", "Producto");
                }
                else
                {
                    // Manejar el caso en el que la eliminación no fue exitosa, como mostrar un mensaje de error o redirigir a una página de error.
                    return View("Error"); // Asegúrate de tener una vista "Error" definida en tu proyecto.
                }
            }
            catch (Exception ex)
            {
                // Manejar errores aquí, como registrarlos o mostrar un mensaje de error al usuario.
                return View("Error");
            }
        }
    }
}
