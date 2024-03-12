using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data
{
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext(DbContextOptions<CollegeDbContext> options): base(options)
        {
            
        }
        public DbSet<Student> Students {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    StudentName = "Mohamad",
                    Email = "m@gmail.com",
                    Adress = "Nablus",
                    DOB = new DateTime(2022,12,12)
                },
                new Student
                {
                    Id = 2,
                    StudentName = "Faiq",
                    Email = "f@gmail.com",
                    Adress = "Jenin",
                    DOB = new DateTime(2022,4,4)
                }
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(n => n.StudentName).IsRequired().HasMaxLength(50);
                entity.Property(n => n.Email).IsRequired().HasMaxLength(150);
                entity.Property(n => n.Adress).IsRequired(false).HasMaxLength(500);
            });
        }
    }
}
