using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Json;

namespace service.Models
{
    public class PuzzleContext : DbContext
    {
        public PuzzleContext(DbContextOptions<PuzzleContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Puzzle> Puzzles { get; set; } = null!;
        public virtual DbSet<UserCredentials> UserCredentials { get; set; } = null!;
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {

            var builderConfiguration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builderConfiguration.Build();

            var con = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(con);
        }
    }
}