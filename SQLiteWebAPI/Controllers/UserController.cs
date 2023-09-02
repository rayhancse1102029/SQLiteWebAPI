using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLiteWebAPI.Data;
using SQLiteWebAPI.Models.Core;

namespace SQLiteWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(User item)
        {
            try
            {
                if (item is not null)
                {
                    await _context.Users.AddAsync(item);
                    await _context.SaveChangesAsync();

                    return Ok(item);
                }
                else
                {
                    return BadRequest("invalid data");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet]
        [Route("GetAllUserById")]
        public async Task<IActionResult> GetAllUserById(int id)
        {
            return Ok(await _context.Users.FirstOrDefaultAsync(x=>x.Id == id));
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> GetAllUserById(User user)
        {
            try
            {
                if (user is not null && user.Id > 0)
                {
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();

                    return Ok(user);
                }
                else
                {
                    return BadRequest("invalid data");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteUserById")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if(user is not null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            else
            {
                return BadRequest("Remove operation is not possible");
            }
           
        }
    }
}
