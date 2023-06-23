using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestApp1.Data;
using TestApp1.Models;
using TestApp1.Models.DTOs;

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
        public async Task<IActionResult> GetUsers()
        {
            var users = await (from u in _dbContext.Users
                               .Include(u=>u.Address)
                               select new UserDTO()
                               {
                                   Id = u.Id,
                                   Index = u.Index,
                                   Age = u.Age,
                                   EyeColor = u.EyeColor,
                                   Name = u.Name,
                                   Gender = u.Gender,
                                   Company = u.Company,
                                   Email = u.Email,
                                   Phone = u.Phone,
                                   Address= (u.Address!=null)?new AddressDTO()
                                   {
                                       Number= u.Address.Number,
                                       Street = u.Address.Street,
                                       City = u.Address.City,
                                       State = u.Address.State,
                                       Zipcode = u.Address.Zipcode
                                   }:null,
                                   About = u.About,
                                   Registered = u.Registered,
                                   Latitude = u.Latitude,
                                   Longitude = u.Longitude,
                                   Tags = u.Tags
                               }).ToListAsync();
            return Ok(users);
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsersWithId(int id)
        {
            var users = await (from u in _dbContext.Users
                               .Include(u => u.Address)
                               where u.Id == id
                               select new UserDTO()
                               {
                                   Id = u.Id,
                                   Index = u.Index,
                                   Age = u.Age,
                                   EyeColor = u.EyeColor,
                                   Name = u.Name,
                                   Gender = u.Gender,
                                   Company = u.Company,
                                   Email = u.Email,
                                   Phone = u.Phone,
                                   Address = (u.Address != null) ? new AddressDTO()
                                   {
                                       Number = u.Address.Number,
                                       Street = u.Address.Street,
                                       City = u.Address.City,
                                       State = u.Address.State,
                                       Zipcode = u.Address.Zipcode
                                   } : null,
                                   About = u.About,
                                   Registered = u.Registered,
                                   Latitude = u.Latitude,
                                   Longitude = u.Longitude,
                                   Tags = u.Tags
                               }).ToListAsync();
           return Ok(users);
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
        public async Task<IActionResult> Put(int id, [FromBody] User userObj)
        {
            var user = await _dbContext.Users.FindAsync(id);
            var address = await _dbContext.Addresses.FindAsync(id);

            if (user != null)
            {
                user.Index = userObj.Index;
                user.Age = userObj.Age;
                user.EyeColor = userObj.EyeColor;
                user.Name = userObj.Name;
                user.Gender = userObj.Gender;
                user.Company = userObj.Company;
                user.Email = userObj.Email;
                user.Phone = userObj.Phone;

                if (address != null)
                {
                    address.Number = userObj.Address!.Number;
                    address.Street = userObj.Address.Street;
                    address.City = userObj.Address.City;
                    address.State = userObj.Address.State;
                    address.Zipcode = userObj.Address.Zipcode;
                }

                user.About = userObj.About;
                user.Registered = userObj.Registered;
                user.Latitude = userObj.Latitude;
                user.Longitude = userObj.Longitude;
                user.Tags = userObj.Tags;

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
            var address = await _dbContext.Addresses.FindAsync(id);

            if (user != null)
            {
                _dbContext.Users.Remove(user);
                if (address != null)
                {
                    _dbContext.Addresses.Remove(address);
                }
                await _dbContext.SaveChangesAsync();
                return Ok("User record deleted succesfully!");
            }
            else
            {
                return NotFound("No User records found for the provided Id ");
            }

        }
    }
}



