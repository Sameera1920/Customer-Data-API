using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp1.Data;
using TestApp1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ApplicationDbContext _dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _dbContext.Users;
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public IEnumerable<User> Get(int id)
        {
            var user = _dbContext.Users.Find(id);
            if (user!= null)
            {
                yield return user;
            }
        }

        // POST api/Users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            var userObj = await _dbContext.Users.FindAsync(id);
            if (userObj !=null)
            {
                userObj.index = user.index;
                userObj.age = user.age;
                userObj.eyeColor = user.eyeColor;
                userObj.name = user.name;
                userObj.gender = user.gender;
                userObj.company = user.company;
                userObj.email = user.email;
                userObj.phone = user.phone;
                await _dbContext.SaveChangesAsync();
                return Ok("Record updated succesfully!");

            }
            else
            {
                return NotFound("No records found for the provided Id ");
            }
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return Ok("Record deleted succesfully!");
            }
            else
            {
                return NotFound("No records found for the provided Id ");
            }
        }
    }
}



