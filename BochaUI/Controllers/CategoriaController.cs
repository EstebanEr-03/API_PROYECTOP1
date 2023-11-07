using BochaUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace BochaUI.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public CategoriaController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Categoria> respuesta = new List<Categoria>();
            try
            {
                var cliente = httpClientFactory.CreateClient();
                var httpRespuesta = await cliente.GetAsync("https://localhost:7042/api/Categoria");
                httpRespuesta.EnsureSuccessStatusCode();
                respuesta.AddRange(await httpRespuesta.Content.ReadFromJsonAsync<IEnumerable<Categoria>>());
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
            List<Categoria> respuesta = new List<Categoria>();
            try
            {
                var cliente = httpClientFactory.CreateClient();
                var httpRespuesta = await cliente.GetAsync("https://localhost:7042/api/Categoria");
                httpRespuesta.EnsureSuccessStatusCode();
                respuesta.AddRange(await httpRespuesta.Content.ReadFromJsonAsync<IEnumerable<Categoria>>());
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(respuesta);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid IdCategoria)
        {

            var cliente = httpClientFactory.CreateClient();
            var respuesta = await cliente.GetFromJsonAsync<Categoria>($"https://localhost:7042/api/Categoria/{IdCategoria.ToString()}");

            if (respuesta is not null)
            {
                return View(respuesta);
            }
            return View(null);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid IdCategoria)
        {

            var cliente = httpClientFactory.CreateClient();
            var respuesta = await cliente.GetFromJsonAsync<Categoria>($"https://localhost:7042/api/Categoria/{IdCategoria.ToString()}");

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
        public async Task<IActionResult> Create(Categoria solicitud)
        {

            var cliente = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7042/api/Categoria/"),
                Content = new StringContent(JsonSerializer.Serialize(solicitud), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await cliente.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respuesta = await httpResponseMessage.Content.ReadFromJsonAsync<Producto>();

            if (respuesta is not null)
            {
                return RedirectToAction("Resumen", "Categoria");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Categoria solicitud)
        {
            var cliente = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7042/api/Categoria/{solicitud.IdCategoria.ToString()}"),
                Content = new StringContent(JsonSerializer.Serialize(solicitud), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await cliente.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respuesta = await httpResponseMessage.Content.ReadFromJsonAsync<Categoria>();

            return RedirectToAction("Resumen", "Categoria");

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid IdCategoria)
        {
            try
            {
                var cliente = httpClientFactory.CreateClient();
                var httpResponseMessage = await cliente.DeleteAsync($"https://localhost:7042/api/Categoria/{IdCategoria}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    // La eliminación fue exitosa, puedes redirigir al usuario a la página de resumen o realizar otra acción apropiada.
                    return RedirectToAction("Resumen", "Categoria");
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
