using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entity;
using Store.Repository.InterFace;
using Store.Repository.Repository;
using Store.Repository.Specification;
using Store.Service.Services.product;
using Store.Service.Services.product.Dtos;

namespace Store.API.Controllers
{

    public class ProductController : BassApiController
    {
        private readonly IGenaricRepo<Product> _genaricRepo;
        
        
        // Constructor: Inject the generic repository for Product
        public ProductController(IGenaricRepo<Product> productRepo)
        {
            _genaricRepo = productRepo;
        }

        // /api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            // Apply specification with filters for brand and category
            var spec = new ProductSpecification();
            var product = await _genaricRepo.GetAllWithSpecAsync(spec);
            return Ok(product);
        }
        // /api/Product/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            // Apply specification to fetch the product by its ID
            var spec = new ProductSpecification(id);
            var productById = await _genaricRepo.GetWithSpecAsync(spec);
            
            if (productById == null)
                return NotFound();// Return 404 if the product doesn't exist  

            return Ok(productById);// Return the product if found
        }




    }
}
