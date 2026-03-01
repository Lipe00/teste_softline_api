
using domain.entidades;
using infraestrutura;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using softline.API.DTOs;

namespace softline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private APIDbContext _db;

        public ClientController(APIDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var clients = _db.Client
                .Select(c => new ResponseClientDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Fantasy_name = c.Fantasy_name,
                    Document = c.Document,
                    Address = c.Address
                }).ToList();
            return Ok(clients);
        }

        [Authorize]
        [HttpGet("paged")]
        public IActionResult GetPaged([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            var query = _db.Client
                .OrderBy(c => c.Id)
                .Select(c => new ResponseClientDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Fantasy_name = c.Fantasy_name,
                    Document = c.Document,
                    Address = c.Address
                }).ToList();

            if(page.HasValue && pageSize.HasValue)
            {
                var totalItems = query.Count();

                var clients = query
                                .Skip((page.Value - 1) * pageSize.Value)
                                .Take(pageSize.Value)
                                .ToList();
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize.Value);

                return Ok(new
                {
                    data = clients,
                    totalItems,
                    totalPages
                });
            }

            return Ok(query.ToList());
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(CreateClientDTO dto)
        {
            var client = new Client
            {
                Name = dto.Name,
                Fantasy_name = dto.Fantasy_name,
                Document = dto.Document,
                Address = dto.Address
            };
            var clients = _db.Client.Add(client);
            _db.SaveChanges();
            return Ok(clients.Entity);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var client = _db.Client.Find(id);

            if(client == null)
                return NotFound();

            return Ok(client);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateClientDTO dto)
        {
            var client = _db.Client.Find(id);

            if (client == null)
                return NotFound();

            if (dto.Name != null)
                client.Name = dto.Name;

            if (dto.Fantasy_name != null)
                client.Fantasy_name = dto.Fantasy_name;

            if (dto.Document != null)
                client.Document = dto.Document;

            if (dto.Address != null)
                client.Address = dto.Address;

            _db.SaveChanges();
            return Ok(client);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var client = _db.Client.Find(id);

            if (client == null)
                return NotFound();
            _db.Client.Remove(client);
            _db.SaveChanges();
            return Ok();
        }
    }
}
