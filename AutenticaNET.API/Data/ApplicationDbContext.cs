using AutenticaNET.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AutenticaNET.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).IsRequired().HasMaxLength(37);
                b.HasIndex(u => u.Email).IsUnique();
                b.HasIndex(u => u.Username).IsUnique();
            });
        }
    }
}
