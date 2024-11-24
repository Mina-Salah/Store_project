using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entity;
using Store.Repository.InterFace;
using Store.Service.Services.product;
using Store.Service.Services.product.Dtos;

namespace Store.API.Controllers
{

    public class ProductController : BassApiController
    {
        private readonly IGenaricRepo<Product> _genaricRepo;

        public ProductController(IGenaricRepo<Product> productRepo)
        {
            _genaricRepo = productRepo;
        }

        // /api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            var product = await _genaricRepo.GetAllAsync();
            return Ok(product);
        }
        // /api/Product/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var productById = await _genaricRepo.GetAsync(id);
            if (productById == null)
                return NotFound();  

            return Ok(productById);
        }




    }
}
