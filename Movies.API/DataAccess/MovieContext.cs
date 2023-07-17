using Microsoft.EntityFrameworkCore;
using Movies.API.Entity;

namespace Movies.API.DataAccess
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
    }
}