using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CineVault.Models
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
