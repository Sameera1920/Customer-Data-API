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
            var users = await (
                               from u in _dbContext.Users
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
                                    Address=u.Address,
                                    About = u.About,
                                    Registered = u.Registered,
                                    Latitude = u.Latitude,
                                    Longitude = u.Longitude,
                                    //Tags = u.Tags
                                 }).ToListAsync();

            //var addresses = await (from a in _dbContext.Addresses
            //                   select new AddressDTO()
                         
            //            {
            //                Number = a.Number,
            //                Street = a.Street,
            //                City =a.City,
            //                State = a.State,
            //                Zipcode = a.Zipcode
            //            }).ToListAsync();

            return Ok(users);
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var users = await (from user in _dbContext.Users.Where(u=>u.Id==id)
                               select new
                               {
                                   Id = user.Id,
                                   Index = user.Index,
                                   Age = user.Age,
                                   EyeColor = user.EyeColor,
                                   Name = user.Name,
                                   Gender = user.Gender,
                                   Company = user.Company,
                                   Email = user.Email,
                                   Phone = user.Phone,
                                   Address = user.Address,
                                   About = user.About,
                                   Registered = user.Registered,
                                   Latitude = user.Latitude,
                                   Longitude = user.Longitude,
                                   //Tags = user.Tags

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
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            var userObj = await _dbContext.Users.FindAsync(id);
            var addressObj = await _dbContext.Addresses.FindAsync(id);

            if (userObj !=null)
            {
                userObj.Index = user.Index;
                userObj.Age = user.Age;
                userObj.EyeColor = user.EyeColor;
                userObj.Name = user.Name;
                userObj.Gender = user.Gender;
                userObj.Company = user.Company;
                userObj.Email = user.Email;
                userObj.Phone = user.Phone;

                if (addressObj != null)
                {
                    addressObj.Number = user.Address.Number;
                    addressObj.Street = user.Address.Street;
                    addressObj.City = user.Address.City;
                    addressObj.State = user.Address.State;
                    addressObj.Zipcode = user.Address.Zipcode;
                }

                userObj.About = user.About;
                userObj.Registered = user.Registered;
                userObj.Latitude = user.Latitude;
                userObj.Longitude = user.Longitude;
                //userObj.Tags = user.Tags;

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



