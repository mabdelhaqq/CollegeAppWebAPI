using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CollegeApp.Data
{
    public class CollegeDbContext : IdentityDbContext<IdentityUser>
    {
        public CollegeDbContext(DbContextOptions<CollegeDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Student>(entity =>
            {
                entity.Property(n => n.StudentName).IsRequired().HasMaxLength(50);
                entity.Property(n => n.Email).IsRequired().HasMaxLength(150);
                entity.Property(n => n.Adress).IsRequired(false).HasMaxLength(500);
            });
            SeedRole(builder);
        }
        private static void SeedRole(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name = "Superadmin", ConcurrencyStamp = "1", NormalizedName = "Superadmin" },
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "2", NormalizedName = "Admin" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "3", NormalizedName = "User" }
                );
        }
        public DbSet<Student> Students {  get; set; }
    }
}
