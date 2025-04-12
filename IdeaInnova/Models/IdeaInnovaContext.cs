using Microsoft.EntityFrameworkCore;

namespace IdeaInnova.Models
{
    public class IdeaInnovaContext : DbContext
    {
        public IdeaInnovaContext(DbContextOptions<IdeaInnovaContext> options) : base(options) { }

        public DbSet<Idea> Ideas { get; set; }
        public DbSet<User> Users { get; set; }
    }
}