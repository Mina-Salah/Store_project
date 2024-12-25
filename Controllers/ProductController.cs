using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.API.Dtos;
using Store.API.Error;
using Store.Data.Entity;
using Store.Repository.InterFace;
using Store.Repository.Repository;
using Store.Repository.Specification.product;

namespace Store.API.Controllers
{
    public class ProductController : BassApiController
    {
        private readonly IGenaricRepo<Product> _productRepo;
        private readonly IMapper _mapper;

        // Constructor: Inject the generic repository for Product
        public ProductController(IGenaricRepo<Product> productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        // /api/Product
        [ProducesResponseType(typeof(IEnumerable<ProductToReturnDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts([FromQuery] ProductQueryParameters parameters)
        {
            var spec = new ProductSpecification(parameters);
            var products = await _productRepo.GetAllWithSpecAsync(spec);

            if (!products.Any())
            {
                return NotFound(new ApiResponse(404));
            }

            var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products);

            return Ok(mappedProducts);
        }

        // /api/Product/id
        [ProducesResponseType(typeof(ProductToReturnDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetById(int id)
        {
            // Apply specification to fetch the product by its ID
            var spec = new ProductSpecification(id);
            var productById = await _productRepo.GetWithSpecAsync(spec);

            if (productById == null)
                return NotFound(new ApiResponse(404)); // Return 404 if the product doesn't exist  

            return Ok(_mapper.Map<Product, ProductToReturnDto>(productById)); // Return the product if found
        }
    }
}
