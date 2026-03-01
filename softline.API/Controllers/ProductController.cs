using domain.entidades;
using infraestrutura;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using softline.API.DTOs;

namespace softline.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private APIDbContext _db;

        public ProductController(APIDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var products = _db.Product
                .Select(p => new ResponseProductDTO
                {
                    Id = p.Id,
                    Code = p.Code,
                    Description = p.Description,
                    BarCode = p.BarCode,
                    Price = p.Price,
                    GrossWeight = p.GrossWeight,
                    NetWeight = p.NetWeight
                }).ToList();
            return Ok(products);
        }

        [Authorize]
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

            _db.Product.Add(product);
            _db.SaveChanges();

            var result = new ResponseProductDTO
            {
                Id = product.Id,
                Code = product.Code,
                Description = product.Description,
                BarCode = product.BarCode,
                Price = product.Price,
                GrossWeight = product.GrossWeight,
                NetWeight = product.NetWeight
            };

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _db.Product.Find(id);

            if (product == null)
                return NotFound();

            var result = new ResponseProductDTO
            {
                Id = product.Id,
                Code = product.Code,
                Description = product.Description,
                BarCode = product.BarCode,
                Price = product.Price,
                GrossWeight = product.GrossWeight,
                NetWeight = product.NetWeight
            };

            return Ok(result); ;
        }

        [Authorize]
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

            var result = new ResponseProductDTO
            {
                Id = product.Id,
                Code = product.Code,
                Description = product.Description,
                BarCode = product.BarCode,
                Price = product.Price,
                GrossWeight = product.GrossWeight,
                NetWeight = product.NetWeight
            };

            return Ok(result);
        }

        [Authorize]
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
