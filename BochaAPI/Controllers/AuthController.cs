using BochaAPI.Models.DTO;
using BochaAPI.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BochaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepositorio tokenRepositorio;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepositorio tokenRepositorio)
        {
            this.userManager = userManager;
            this.tokenRepositorio = tokenRepositorio;
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };

            var identityResult=await userManager.CreateAsync(identityUser, registerRequestDTO.Password);
            

            if (identityResult.Succeeded) 
            {
                //Add roles si es succes
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("Usuario registrado correctamente, porfavor loggeate");
                    }
                }
            }
            return BadRequest("Algo ha salido mal");

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user=await userManager.FindByEmailAsync(loginRequestDTO.Username);

            if (user != null) 
            {
                var checkPassword=await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);


                if (checkPassword) 
                {
                    //Get Roles for this user
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles !=null)
                    {
                        //CREATE TOKEN
                        var jwtToken = tokenRepositorio.CreateJWTTOken(user, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);

                    }
                    
                }
            }
            return BadRequest("User or Password Incorrect");
        }
    }
}
