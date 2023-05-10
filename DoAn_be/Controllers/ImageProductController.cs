using Azure.Core;
using DoAn_be.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAn_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageProductController : ControllerBase
    {
        private readonly DatabaseDoanContext _context;

        public ImageProductController(DatabaseDoanContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ImageProduct>>> Get()
        {
            return Ok(await _context.ImageProducts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImageProduct>> Get(int id)
        {
            var found_id = await _context.ImageProducts.FindAsync(id);
            if (found_id == null)
            {
                return BadRequest("ImageProduct not found");
            }
            return Ok(found_id);
        }

        [HttpPost]
        public async Task<ActionResult<List<ImageProduct>>> AddImageProduct([FromBody] ImageProduct imageProduct)
        {
            if (ImageProductExists(imageProduct.ImageNameProduct) != null)
            {
                return Ok(new
                {
                    message = "Existed ImageNameProduct!"
                });
            }

            _context.ImageProducts.Add(imageProduct);
            await _context.SaveChangesAsync();
            return Ok(await _context.ImageProducts.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<ImageProduct>>> UpdateImageProduct(ImageProduct request)
        {
            var dbImageProduct = await _context.ImageProducts.FindAsync(request.IdImageProduct);
            if (dbImageProduct == null)
            {
                return BadRequest("ImageProduct not found");
            }

            dbImageProduct.ImageNameProduct = request.ImageNameProduct;
            dbImageProduct.IdProduct= request.IdProduct;
            await _context.SaveChangesAsync();

            return Ok(await _context.ImageProducts.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ImageProduct>>> DeleteImageProduct(int id)
        {
            var dbImageProduct = await _context.ImageProducts.FindAsync(id);
            if (dbImageProduct == null)
            {
                return BadRequest("ImageProduct not found");
            }

            _context.ImageProducts.Remove(dbImageProduct);
            await _context.SaveChangesAsync();
            return Ok(await _context.ImageProducts.ToListAsync());
        }

        private bool ImageProductExists(int id)
        {
            return (_context.ImageProducts?.Any(e => e.IdImageProduct == id)).GetValueOrDefault();
        }
        private ImageProduct ImageProductExists(string imageproductname)
        {
            var ImageProduct = _context.ImageProducts.FirstOrDefault(e => e.ImageNameProduct == imageproductname);
            return ImageProduct;
        }

    }
}
