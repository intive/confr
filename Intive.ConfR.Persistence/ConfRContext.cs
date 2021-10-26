using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Intive.ConfR.Persistence
{
    public class ConfRContext : DbContext
    {
        public ConfRContext(DbContextOptions<ConfRContext> options) 
            : base(options) {}

        public DbSet<PhotoUrl> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new PhotoConfiguration())
                .ApplyConfiguration(new CommentConfiguration());
        }

    }
}
