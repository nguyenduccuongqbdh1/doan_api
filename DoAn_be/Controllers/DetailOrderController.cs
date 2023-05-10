using DoAn_be.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAn_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailOrderController : ControllerBase
    {
        private readonly DatabaseDoanContext _context;

        public DetailOrderController(DatabaseDoanContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<DetailOrder>>> Get()
        {
            return Ok(await _context.DetailOrders.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailOrder>> Get(int id)
        {
            var found_id = await _context.DetailOrders.FindAsync(id);
            if (found_id == null)
            {
                return BadRequest("DetailOrders not found");
            }
            return Ok(found_id);
        }

        [HttpPost]
        public async Task<ActionResult<List<DetailOrder>>> AddDetailOrder([FromBody] DetailOrder detailOrder)
        {
            _context.DetailOrders.Add(detailOrder);
            await _context.SaveChangesAsync();
            return Ok(await _context.DetailOrders.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<DetailOrder>>>UpdateDetailOrder(DetailOrder request)
        {
            var dbDetailOrder = await _context.DetailOrders.FindAsync(request.IdDetailOrders);
            if (dbDetailOrder == null)
            {
                return BadRequest("DetailOrder not found");
            }

            dbDetailOrder.QuantityDetailOrders = request.QuantityDetailOrders;
            dbDetailOrder.PriceDetailOrders= request.PriceDetailOrders;
            dbDetailOrder.IdOrders = request.IdOrders;
            await _context.SaveChangesAsync();

            return Ok(await _context.DetailOrders.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<DetailOrder>>> DeleteDetailOrder(int id)
        {
            var dbDetailOrder = await _context.DetailOrders.FindAsync(id);
            if (dbDetailOrder == null)
            {
                return BadRequest("DetailOrder not found");
            }
            _context.DetailOrders.Remove(dbDetailOrder);
            await _context.SaveChangesAsync();
            return Ok(await _context.DetailOrders.ToListAsync());
        }
    }
}
