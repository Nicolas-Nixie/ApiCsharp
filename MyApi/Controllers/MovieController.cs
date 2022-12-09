using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext2 dbContext;

        public MovieController(ApplicationDbContext2 dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await dbContext.Movies.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Movie>>> CreateMovie([FromBody] Movie movie)
        {
            dbContext.Movies.Add(Movie);
            await dbContext.SaveChangesAsync();

            return Ok(dbContext.Movies.ToList());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Movie>>> DeleteMovie([FromBody] Movie movie)
        {
            dbContext.Movies.Remove(Movie);
            await dbContext.SaveChangesAsync();

            return Ok(await dbContext.Movies.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Movie>>> UpdateMovie(Movie request)
        {
            var movie = dbContext.Movies.Find(request.Id);
            if (movie == null)
            {
                return BadRequest(request);
            }

            movie.Title = request.Title;
            movie.During = request.During;
            movie.Year = request.Year;
            movie.Director = request.Director;
            await dbContext.SaveChangesAsync();

            return await dbContext.Movies.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> Get(int id)
        {
            var movie = dbContext.Movies.Find(id);
            if (movie == null)
            {
                return NotFound("Movie not Found");
            }
            return Ok(movie);
        }
    }
}