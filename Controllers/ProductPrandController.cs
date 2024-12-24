using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entity;
using Store.Repository.InterFace;

namespace Store.API.Controllers
{
   
    public class ProductPrandController : BassApiController
    {
        private readonly IGenaricRepo<ProductBrand> _brand;

        public ProductPrandController(IGenaricRepo<ProductBrand> brand)
        {
            _brand = brand;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAll()
        {
            var brand = await _brand.GetAllAsync();
            return Ok(brand);
        }

        // Get category by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetById(int id)
        {
            var brand = await _brand.GetAsync(id);
            if (brand == null)
                return NotFound();

            return Ok(brand);
        }
    }
}
