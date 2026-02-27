
using domain.entidades;
using infraestrutura;
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
        [HttpGet]
        public IActionResult Get()
        {
            var clients = _db.Client.ToList();
            return Ok(clients);
        }
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
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var client = _db.Client.Find(id);

            if(client == null)
                return NotFound();

            return Ok(client);
        }

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
