using DoAn_be.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Formats.Asn1;
using System.Threading.Tasks;

namespace DoAn_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DatabaseDoanContext _context;

        public AdminController(DatabaseDoanContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Admin>>> Get()
        {
            return Ok(await _context.Admins.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> Get(int id)
        {
            var found_id = await _context.Admins.FindAsync(id);
            if (found_id == null)
            {
                return BadRequest("Admin not found");
            }
            return Ok(found_id);
        }

        [HttpPost]
        public async Task<ActionResult<List<Admin>>> AddAdmin([FromBody] Admin admin) 
        {
            if (AdminExists(admin.AccountAdmins) != null) 
            {
                return Ok(new
                { 
                    message = "Existed AccountAdmin!"
                });
            }

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return Ok(await _context.Admins.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Admin>>> UpdateAdmin(Admin request) 
        {
            var dbAdmin = await _context.Admins.FindAsync(request.IdAdmins);
            if (dbAdmin == null) 
            { 
                return BadRequest("Admin not found");
            }

            dbAdmin.PasswordAdmins = request.PasswordAdmins;
            await _context.SaveChangesAsync();

            return Ok(await _context.Admins.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Admin>>> DeleteAdmin(int id)
        {
            var dbAdmin = await _context.Admins.FindAsync(id);
            if (dbAdmin == null) 
            {
                return BadRequest("Admin not found");
            }
            _context.Admins.Remove(dbAdmin);
            await _context.SaveChangesAsync();
            return Ok(await _context.Admins.ToListAsync());
        }


        private bool AdminExists(int id)
        {
            return (_context.Admins?.Any(e => e.IdAdmins == id)).GetValueOrDefault();
        }
        private Admin AdminExists(string username)
        {
            var admin = _context.Admins.FirstOrDefault(e => e.AccountAdmins == username);
            return admin;
        }
    }
}
