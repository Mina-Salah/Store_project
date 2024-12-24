using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.API.Error;
using Store.Data.Entity;
using Store.Repository.InterFace;

namespace Store.API.Controllers
{
   
    public class ProductCategoryController : BassApiController
    {
        private readonly IGenaricRepo<ProductCategory> _category;

        public ProductCategoryController(IGenaricRepo<ProductCategory> category)
        {
            
            _category = category;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> Getall()
        {
            var category = await _category.GetAllAsync();
            return Ok(category);    
        }

        // Get category by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetById(int id)
        {
            var category = await _category.GetAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

    }
}
