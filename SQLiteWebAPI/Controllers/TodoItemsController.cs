using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLiteWebAPI.Data;
using SQLiteWebAPI.Models.Core;

namespace SQLiteWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("CreateTodoItem")]
        public async Task<IActionResult> CreateTodoItem(TodoItem item)
        {
            try
            {
                if(item is not null)
                {
                    await _context.TodoItems.AddAsync(item);
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
        [Route("GetAllTodoItems")]
        public async Task<IActionResult> GetAllTodoItems()
        {
            return Ok(await _context.TodoItems.ToListAsync());
        }
    }
}
