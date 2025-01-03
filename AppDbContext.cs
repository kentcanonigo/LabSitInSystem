using Microsoft.EntityFrameworkCore;

namespace LaboratorySitInSystem {
    public class AppDbContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<SitIn> SitIns { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=\"C:\\Users\\Beast\\source\\repos\\LabSitInSystem\\LaboratorySitInSystem.db\"");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Seed an initial admin user
            modelBuilder.Entity<User>().HasData(new User {
                UserId = 1, // Ensure this is unique
                Username = "admin@ctu.edu.ph", // Admin username
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), // Replace "admin123" with the actual password
                FullName = "Administrator" // Optional: Full name of the admin
            });

            modelBuilder.Entity<SitIn>()
                .HasOne(s => s.Student)
                .WithMany()
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class User {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
    }

    public class Student {
        public string StudentId { get; set; }
        public string FullName { get; set; }
        public string Program { get; set; }
        public string Year { get; set; }
        public string Section { get; set; }
        public string PasswordHash { get; set; }
    }


    public class SitIn {
        public int SitInId { get; set; }
        public string StudentId { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public int? Duration { get; set; } // In minutes
        public bool ApprovedByAdmin { get; set; }

        public Student Student { get; set; }
    }

    public class Notification {
        public int NotificationId { get; set; }
        public int SitInId { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime NotificationTime { get; set; }

        public SitIn SitIn { get; set; }
    }
}
