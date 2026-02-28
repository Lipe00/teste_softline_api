using domain.entidades;
using Microsoft.AspNetCore.Mvc;
using softline.API.Services;

namespace softline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if(username == "teste" && password == "teste123")
            {
                var token = TokenService.GenerateToken(new User());
                return Ok(token);
            }
            return BadRequest("Nome de usuário ou senha incorretos");
        }
    }
}
