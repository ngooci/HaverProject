using Microsoft.EntityFrameworkCore;
using HaverProject.Models;

namespace HaverProject.Data
{
    public class HaverContext : DbContext
    {
        public HaverContext(DbContextOptions<HaverContext> options)
            : base(options)
        {
        }
        public DbSet<NCR> Ncrs { get; set; }
      
    }
}
