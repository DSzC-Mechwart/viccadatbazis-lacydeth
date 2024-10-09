using Microsoft.EntityFrameworkCore;

namespace ViccAdatbazis.Models
{
    public class ViccDbContext : DbContext
    {
        public ViccDbContext(DbContextOptions<ViccDbContext> options) : base(options) { }

        public DbSet<Vicc> Viccek {  get; set; }
    }
}
