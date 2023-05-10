using Azure.Core;
using DoAn_be.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DoAn_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DatabaseDoanContext _context;

        public OrderController(DatabaseDoanContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get()
        {
            return Ok(await _context.Orders.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        { 
            var found_id = await _context.Orders.FindAsync(id);
            if (found_id == null)
            {
                return BadRequest("Order not found");
            }
            return Ok(found_id);
        }

        [HttpPost]
        public async Task<ActionResult<List<Order>>> AddOrder([FromBody] Order order)
        { 
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok(await _context.Orders.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Order>>> UpdateOrder(Order request)
        { 
            var dbOrder = await _context.Orders.FindAsync(request.IdOrders);
            if (dbOrder == null)
            {
                return BadRequest("Order not found");
            }

            dbOrder.Statuss = request.Statuss;
            await _context.SaveChangesAsync();

            return Ok(await _context.Orders.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Order>>> DeleteOrder(int id)
        {
            var dbOrder = await _context.Orders.FindAsync(id);
            if (dbOrder == null)
            {
                return BadRequest("Order not found");
            }

            _context.Orders.Remove(dbOrder);
            await _context.SaveChangesAsync();
            return Ok(await _context.Orders.ToListAsync());
        }
    }
}
