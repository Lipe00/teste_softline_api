using domain.entidades;
using infraestrutura;
using Microsoft.AspNetCore.Mvc;
using softline.API.DTOs;
using softline.API.Services;

namespace softline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly APIDbContext _db;

        public AuthController(APIDbContext db, TokenService tokenService)
        {
            _db = db;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var user = _db.User.FirstOrDefault(x => x.Name == dto.Name);

            if (user == null)
                return Unauthorized("Usuário ou senha inválidos");

            var senhaValida = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            if (!senhaValida)
                return Unauthorized("Usuário ou senha inválidos");

            var token = TokenService.GenerateToken(user);

            return Ok(token);
        }
    }
}