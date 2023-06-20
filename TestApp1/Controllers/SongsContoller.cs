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
        public void Post([FromBody] Song song)
        {
            _dbContext.Add(song);
            _dbContext.SaveChanges();
        }

        // PUT api/Songs/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Song song)
        {
            var songObj = _dbContext.Songs.Find(id);
            if (songObj !=null)
            {
                songObj.Title = song.Title;
                songObj.Language = song.Language;
                _dbContext.SaveChanges();
                return Ok("Record updated succesfully!");

            }
            else
            {
                return NotFound("No records found for the provided Id ");
            }
        }

        // DELETE api/Songs/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var song = _dbContext.Songs.Find(id);
            if (song != null)
            {
                _dbContext.Songs.Attach(song);
                _dbContext.Songs.Remove(song);
                _dbContext.SaveChanges();
                return Ok("Record deleted succesfully!");
            }
            else
            {
                return NotFound("No records found for the provided Id ");
            }
        }
    }
}



