
using Microsoft.EntityFrameworkCore;
using MoviesManagement.Domain.Entities;

namespace MoviesManagement.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }
    }
}
