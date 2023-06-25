﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestApp1.Data;
using TestApp1.Models;
using TestApp1.Models.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


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
                var users = await _dbContext.Users
                    .Include(u => u.Address)
                    .Select(u => MapUserToDTO(u))
                    .ToListAsync();

                return Ok(users);
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsersWithId(int id)
        {
            var user = await _dbContext.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound("No User records found for the provided Id");

            var userDTO = MapUserToDTO(user);
            return Ok(userDTO);
        }

        // POST api/Users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        //// PUT api/Users/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, [FromBody] User userObj)
        //{
        //    var user = await _dbContext.Users.FindAsync(id);
        //    if (user == null)
        //        return NotFound("No records found for the provided Id");

        //    await _dbContext.SaveChangesAsync();
        //    return Ok("Record updated successfully!");
        //}

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

        private static UserDTO MapUserToDTO(User user)
        {
            var userDTO = new UserDTO();
            var userProperties = typeof(UserDTO).GetProperties();

            foreach (var userProperty in userProperties)
            {
                var userPropertyName = userProperty.Name;
                var userPropertyInfo = typeof(User).GetProperty(userPropertyName);
                var userPropertyValue = userPropertyInfo?.GetValue(user);

                if (userPropertyName == nameof(UserDTO.Address))
                {
                    var addressDTO = MapAddressToDTO((Address) userPropertyValue);
                    userProperty.SetValue(userDTO, addressDTO);
                }
                else
                {
                    userProperty.SetValue(userDTO, userPropertyValue);
                }
            }

            return userDTO;
        }

        private static AddressDTO? MapAddressToDTO(Address address)
        {
            if (address == null)
                return null;

            var addressDTO = new AddressDTO();
            var addressProperties = typeof(AddressDTO).GetProperties();

            foreach (var addressProperty in addressProperties)
            {
                var addressPropertyName = addressProperty.Name;
                var addressPropertyInfo = typeof(Address).GetProperty(addressPropertyName);
                var addressPropertyValue = addressPropertyInfo?.GetValue(address);

                addressProperty.SetValue(addressDTO, addressPropertyValue);
            }

            return addressDTO;
        }


    }
}



