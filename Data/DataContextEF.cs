using DotnetAPI.Models; // Importing the User, UserSalary, and UserJobInfo models
using Microsoft.EntityFrameworkCore; // Importing the Entity Framework Core namespace

namespace DotnetAPI.Data
{
    // DataContextEF class represents the database context for Entity Framework Core
    public class DataContextEF : DbContext
    {
        private readonly IConfiguration _config; // Configuration object for database connection

        // Constructor to initialize DataContextEF with IConfiguration
        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        // DbSet properties representing database tables
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSalary> UserSalary { get; set; }
        public virtual DbSet<UserJobInfo> UserJobInfo { get; set; }

        // Configuring database connection in OnConfiguring method
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(_config.GetConnectionString("DefaultConnection"), // Using SQL Server database
                        optionsBuilder => optionsBuilder.EnableRetryOnFailure()); // Enabling retry on failure
            }
        }

        // Configuring database model in OnModelCreating method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setting default database schema
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            // Configuring primary keys and table names for each entity
            modelBuilder.Entity<User>()
                .ToTable("Users", "TutorialAppSchema") // Mapping User entity to Users table in TutorialAppSchema
                .HasKey(u => u.UserId); 

            modelBuilder.Entity<UserSalary>()
                .HasKey(u => u.UserId); 

            modelBuilder.Entity<UserJobInfo>()
                .HasKey(u => u.UserId); 

        }
}
}
