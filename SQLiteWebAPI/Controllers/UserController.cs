﻿using Microsoft.AspNetCore.Http;
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
    }
}
