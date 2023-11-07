using BochaUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace BochaUI.Controllers
{
    public class MarcaController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public MarcaController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()    
        {
            List<Marca> respuesta = new List<Marca>();
            try
            {
                var cliente = httpClientFactory.CreateClient();
                var httpRespuesta = await cliente.GetAsync("https://localhost:7042/api/Marca");
                httpRespuesta.EnsureSuccessStatusCode();
                respuesta.AddRange(await httpRespuesta.Content.ReadFromJsonAsync<IEnumerable<Marca>>());
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
            List<Marca> respuesta = new List<Marca>();
            try
            {
                var cliente = httpClientFactory.CreateClient();
                var httpRespuesta = await cliente.GetAsync("https://localhost:7042/api/Marca");
                httpRespuesta.EnsureSuccessStatusCode();
                respuesta.AddRange(await httpRespuesta.Content.ReadFromJsonAsync<IEnumerable<Marca>>());
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(respuesta);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid IdMarca)
        {

            var cliente = httpClientFactory.CreateClient();
            var respuesta = await cliente.GetFromJsonAsync<Marca>($"https://localhost:7042/api/Marca/{IdMarca.ToString()}");

            if (respuesta is not null)
            {
                return View(respuesta);
            }
            return View(null);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid IdMarca)
        {

            var cliente = httpClientFactory.CreateClient();
            var respuesta = await cliente.GetFromJsonAsync<Marca>($"https://localhost:7042/api/Marca/{IdMarca.ToString()}");

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
        public async Task<IActionResult> Create(Marca solicitud)
        {

            var cliente = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7042/api/Marca/"),
                Content = new StringContent(JsonSerializer.Serialize(solicitud), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await cliente.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respuesta = await httpResponseMessage.Content.ReadFromJsonAsync<Marca>();

            if (respuesta is not null)
            {
                return RedirectToAction("Resumen", "Marca");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Marca solicitud)
        {
            var cliente = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7042/api/Marca/{solicitud.IdMarca.ToString()}"),
                Content = new StringContent(JsonSerializer.Serialize(solicitud), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await cliente.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respuesta = await httpResponseMessage.Content.ReadFromJsonAsync<Marca>();

            return RedirectToAction("Resumen", "Marca");

        }
        [HttpPost]
        public async Task<IActionResult> Delete(Marca solicitud)
        {
            var cliente = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"https://localhost:7042/api/Marca/{solicitud.IdMarca.ToString()}")
            };

            var httpResponseMessage = await cliente.DeleteAsync($"https://localhost:7042/api/Marca/{solicitud.IdMarca.ToString()}");
            httpResponseMessage.EnsureSuccessStatusCode();

            var respuesta = await httpResponseMessage.Content.ReadFromJsonAsync<Marca>();

            return RedirectToAction("Resumen", "Marca");
        }
    }
}
