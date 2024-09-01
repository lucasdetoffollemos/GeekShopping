using GeekShopping.Product.API.Data.ValueObjects;
using GeekShopping.Product.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Product.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository)); 

        }

        [HttpGet]
        public async Task<ActionResult<List<ProductVO>>> Get()
        {
            var products = await _productRepository.FindAll();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> Get(long id)
        {
            var product = await _productRepository.FindById(id);

            if(product == null)
                return NotFound();

            if (product.Id <= 0)
                return NotFound();

                return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductVO>> Post(ProductVO vo)
        {
            if(vo == null)  return BadRequest();

            var product = await _productRepository.Create(vo);

            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<ProductVO>> Put(ProductVO vo)
        {
            if (vo == null) return BadRequest();

            var product = await _productRepository.Update(vo);


            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(long id)
        {
            var canDelete = await _productRepository.Delete(id);

            if (!canDelete)
                return Ok("Registro não pode ser deletado");

            return Ok("Registro deletado com sucesso");
        }
    }
}
