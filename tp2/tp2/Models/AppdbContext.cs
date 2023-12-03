using Microsoft.EntityFrameworkCore;
namespace tp2.Models

    /*DB session*/
{
    public class AppdbContext : DbContext
    {
        public DbSet<Movie>? movies { get; set; }
        public DbSet<Genre> genres { get; set; }
        public AppdbContext(DbContextOptions options) : base(options) 
        { }
    }

    }

