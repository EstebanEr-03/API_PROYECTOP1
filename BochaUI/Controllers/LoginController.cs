using BochaUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BochaUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        // GET: LoginController/Login
        public IActionResult Index()
        {
            return View();
        }

        // POST: LoginController/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO loginViewModel)
        {
            if (ModelState.IsValid)
            {
                // Crea una solicitud HTTP POST para iniciar sesión
                var cliente = httpClientFactory.CreateClient();
                var httpRespuesta = await cliente.PostAsync("https://localhost:7042/api/Auth/Login", new StringContent(JsonConvert.SerializeObject(loginViewModel), Encoding.UTF8, "application/json"));

                // Comprueba el estado de la respuesta
                if (httpRespuesta.StatusCode == HttpStatusCode.OK)
                {
                    // El inicio de sesión ha sido exitoso
                    var loginResponseDTO = await httpRespuesta.Content.ReadFromJsonAsync<LoginResponseDTO>();

                    // Guarda el token JWT en el almacenamiento local
                    HttpContext.Session.SetString("JwtToken", loginResponseDTO.JwtToken);

                    // Redirige al usuario a la página de inicio
                    return RedirectToAction("Index", "Producto");
                }
                else
                {
                    // El inicio de sesión ha fallado
                    // Muestra un mensaje de error al usuario
                    ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                }
            }

            return View();
        }
    }
}