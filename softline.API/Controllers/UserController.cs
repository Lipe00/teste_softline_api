using domain.entidades;
using infraestrutura;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Add(User user)
        {
            var users = _db.User.Add(user);
            _db.SaveChanges();
            return Ok(users.Entity);
        }
    }
}
