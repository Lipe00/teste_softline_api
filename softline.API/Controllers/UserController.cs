using domain.entidades;
using infraestrutura;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("firstAuth")]
        public IActionResult FirstAuth(CreateUserDTO dto)
        {
            if (_db.User.Any())
                return BadRequest("O primeiro usuário já foi criado");

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

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _db.User.Select(u => new ResponseUserDTO
            {
                Id = u.Id,
                Name = u.Name
            }).ToList();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("paged")]
        public IActionResult GetPaged([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            var query = _db.User
                .OrderBy(u => u.Id)
                .Select(u => new ResponseUserDTO
            {
                Id = u.Id,
                Name = u.Name
            }).ToList();
            if (page.HasValue && pageSize.HasValue)
            {
                var totalItems = query.Count();

                var users = query
                                .Skip((page.Value - 1) * pageSize.Value)
                                .Take(pageSize.Value)
                                .ToList();
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize.Value);

                return Ok(new
                {
                    data = users,
                    totalItems,
                    totalPages
                });
            }

            return Ok(query.ToList());
        }

        [Authorize]
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

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _db.User.Find(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [Authorize]
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

        [Authorize]
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
