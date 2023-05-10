using DoAn_be.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DoAn_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DatabaseDoanContext _context;

        public ProductController(DatabaseDoanContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var found_id = await _context.Products.FindAsync(id);
            if (found_id == null)
            {
                return BadRequest("Product not found");
            }

            return Ok(found_id);
        }

        [HttpPost]
        public async Task<ActionResult<List<Product>>> AddProduct([FromBody] Product product)
        {
            if (ProductExists(product.NameProduct) != null)
            {
                return Ok(new
                {
                    message = "Existed NameProduct!"
                });
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Product>>> UpdateProduct(Product request)
        {
            var dbProduct = await _context.Products.FindAsync(request.IdProduct);
            if (dbProduct == null)
            {
                return BadRequest("Product not found");
            }

            dbProduct.NameProduct= request.NameProduct;
            dbProduct.DetailProduct= request.DetailProduct;
            dbProduct.QuantityProduct= request.QuantityProduct;
            dbProduct.PriceProduct= request.PriceProduct;
            dbProduct.TagProduct= request.TagProduct;
            dbProduct.IngredientProduct= request.IngredientProduct;
            dbProduct.Hsd = request.Hsd;

            await _context.SaveChangesAsync();

            return Ok(await _context.Products.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
        {
            var dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null)
            {
                return BadRequest("Product not found");
            }

            _context.Products.Remove(dbProduct);
            await _context.SaveChangesAsync();
            return Ok(await _context.Products.ToListAsync());
        }


    private bool ProductExists(int id) 
        {
            return (_context.Products?.Any(e => e.IdProduct == id)).GetValueOrDefault();
        }
        private Product ProductExists(string name)
        {
            var products = _context.Products.FirstOrDefault(e => e.NameProduct == name);
            return products;
        }

    }
}
