using domain.entidades;
using infraestrutura;
using Microsoft.AspNetCore.Mvc;
using softline.API.DTOs;

namespace softline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private APIDbContext _db;

        public ProductController(APIDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        [HttpGet]
        public IActionResult Get()
        {
            var products = _db.Product.ToList();
            return Ok(products);
        }
        [HttpPost]
        public IActionResult Add(CreateProductDTO dto)
        {
            var product = new Product
            {
                Code = dto.Code,
                Description = dto.Description,
                BarCode = dto.BarCode,
                Price = dto.Price,
                GrossWeight = dto.GrossWeight,
                NetWeight = dto.NetWeight
            };

            var products = _db.Product.Add(product);
            _db.SaveChanges();

            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _db.Product.Find(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }
            [HttpPut("{id}")]
            public IActionResult Update(int id, UpdateProductDTO dto)
            {
                var product = _db.Product.Find(id);

                if (product == null)
                    return NotFound();

                if (dto.Code.HasValue)
                    product.Code = dto.Code.Value;

                if (dto.Description != null)
                    product.Description = dto.Description;

                if (dto.BarCode != null)
                    product.BarCode = dto.BarCode;

                if (dto.Price.HasValue)
                    product.Price = dto.Price.Value;

                if (dto.GrossWeight.HasValue)
                    product.GrossWeight = dto.GrossWeight.Value;

                if (dto.NetWeight.HasValue)
                    product.NetWeight = dto.NetWeight.Value;

                _db.SaveChanges();

                return Ok(product);
            }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _db.Product.Find(id);

            if (product == null)
                return NotFound();

            _db.Product.Remove(product);
            _db.SaveChanges();

            return Ok();
        }
    }
}
