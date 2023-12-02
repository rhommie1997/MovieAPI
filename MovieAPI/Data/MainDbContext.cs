using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using System.Runtime.CompilerServices;

namespace MovieAPI.Data
{
    public class MainDbContext : DbContext
    {

        public MainDbContext(DbContextOptions<MainDbContext> op): base(op)
        {

        }
        //public override void OnModelCreating(ModelBuilder mb)
        //{
        //    mb.UseSerialColumns();
        //}
        public DbSet<Movie> Movies { get; set; }

    }
}
