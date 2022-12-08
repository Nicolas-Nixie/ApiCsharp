using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public HeroController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await dbContext.Heroes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Hero>>> CreateHero([FromBody] Hero hero)
        {
            dbContext.Heroes.Add(hero);
            await dbContext.SaveChangesAsync();

            return Ok(dbContext.Heroes.ToList());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Hero>>> DeleteHero([FromBody] Hero hero)
        {
            dbContext.Heroes.Remove(hero);
            await dbContext.SaveChangesAsync();

            return Ok(await dbContext.Heroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Hero>>> UpdateHero(Hero request)
        {
            var hero = dbContext.Heroes.Find(request.Id);
            if (hero == null)
            {
                return BadRequest(request);
            }

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;
            await dbContext.SaveChangesAsync();

            return await dbContext.Heroes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> Get(int id)
        {
            var hero = dbContext.Heroes.Find(id);
            if (hero == null)
            {
                return NotFound("Hero not Found");
            }
            return Ok(hero);
        }
    }
}