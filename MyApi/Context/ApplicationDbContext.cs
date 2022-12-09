using Microsoft.EntityFrameworkCore;

namespace MyApi.Context
{
    public class ApplicationDbContext: DbContext
    {
         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<Hero> Heroes {get; set; }
    }

        public class ApplicationDbContext2: DbContext
    {
         public ApplicationDbContext2(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<Movie> Movies {get; set; }
    }
}