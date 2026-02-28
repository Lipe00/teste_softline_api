using domain.entidades;
using infraestrutura;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using softline.API.DTOs;

namespace softline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private APIDbContext _db;

        public UserController(APIDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        [HttpGet]
        public IActionResult Get()
        {
            var users = _db.User.ToList();
            return Ok(users);
        }
        [HttpPost]
        public IActionResult Add(CreateUserDTO dto)
        {
            if (_db.User.Any(x => x.Name == dto.Name))
                return BadRequest("Usuário já existe");

            var user = new User
            {
                Name = dto.Name,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _db.User.Add(user);
            _db.SaveChanges();

            return Ok(new
            {
                user.Id,
                user.Name
            });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _db.User.Find(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateUserDTO dto)
        {
            var user = _db.User.Find(id);

            if (user == null)
                return NotFound();

            if (dto.Name != null)
                user.Name = dto.Name;

            if (dto.Password != null)
                user.Password = dto.Password;

            _db.SaveChanges();

            return Ok(user);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _db.User.Find(id);

            if (user == null)
                return NotFound();

            _db.User.Remove(user);
            _db.SaveChanges();

            return Ok();
        }
    }
}
