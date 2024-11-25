using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.API.Dtos;
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
        private readonly IGenaricRepo<Product> _productRepo;
        private readonly IMapper _mapper;


        // Constructor: Inject the generic repository for Product
        public ProductController(IGenaricRepo<Product> productRepo , IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        // /api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProduct()
        {
            // Apply specification with filters for brand and category
            var spec = new ProductSpecification();
            var product = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IEnumerable< Product>,IEnumerable< ProductToReturnDto>>(product));
        }
        // /api/Product/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetById(int id)
        {
            // Apply specification to fetch the product by its ID
            var spec = new ProductSpecification(id);
            var productById = await _productRepo.GetWithSpecAsync(spec);
            
            if (productById == null)
                return NotFound();// Return 404 if the product doesn't exist  

            return Ok(_mapper.Map<Product,ProductToReturnDto>(productById));// Return the product if found
        }




    }
}
