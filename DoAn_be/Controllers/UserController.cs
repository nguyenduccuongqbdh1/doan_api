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
    public class UserController : ControllerBase
    {
        private readonly DatabaseDoanContext _context;

        public UserController(DatabaseDoanContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var found_id = await _context.Users.FindAsync(id);
            if (found_id == null)
            {
                return BadRequest("User not found");
            }
            return Ok(found_id);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser([FromBody] User user)
        {
            if (UserExists(user.UsernameUsers) != null)
            {
                return Ok(new
                {
                    message = "Existed AccountAdmin!"
                });
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<List<User>>> UpdateUser(User request)
        {
            var dbUser = await _context.Users.FindAsync(request.IdUsers);
            if (dbUser == null)
            {
                return BadRequest("User not found");
            }

            dbUser.PasswordUsers = request.PasswordUsers;
            dbUser.AddressUsers = request.AddressUsers;
            dbUser.PhoneNumberUsers = request.PhoneNumberUsers;
            dbUser.ImagepathUsers = request.ImagepathUsers;
            //dbUser.

            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var dbUser = await _context.Users.FindAsync(id);
            if (dbUser == null)
            {
                return BadRequest("User not found");
            }

            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }


        private bool UserExists(int id) 
        {
            return (_context.Users?.Any(e => e.IdUsers == id)).GetValueOrDefault();
        }
        private User UserExists(string username) 
        {
            var user = _context.Users.FirstOrDefault(e => e.UsernameUsers== username);
            return user;
        }
    }
}
