using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp1.Data;
using TestApp1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PracticeApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private ApplicationDbContext _dbContext;

        public SongsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Songs
        [HttpGet]
        public IEnumerable<Song> Get()
        {
            return _dbContext.Songs;
        }

        // GET api/Songs/5
        [HttpGet("{id}")]
        public IEnumerable<Song> Get(int id)
        {
            var song = _dbContext.Songs.Find(id);
            if (song!= null)
            {
                yield return song;
            }
            
        }

        // POST api/Songs
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Song song)
        {
            await _dbContext.AddAsync(song);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/Songs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Song song)
        {
            var songObj = await _dbContext.Songs.FindAsync(id);
            if (songObj !=null)
            {
                songObj.Title = song.Title;
                songObj.Language = song.Language;
                await _dbContext.SaveChangesAsync();
                return Ok("Record updated succesfully!");

            }
            else
            {
                return NotFound("No records found for the provided Id ");
            }
        }

        // DELETE api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var song = await _dbContext.Songs.FindAsync(id);
            if (song != null)
            {
                _dbContext.Songs.Remove(song);
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



