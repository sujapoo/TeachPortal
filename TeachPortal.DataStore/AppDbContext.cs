using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechPortal.Models.Models;

namespace TeachPortal.DataStore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the table name for the Teacher entity
            modelBuilder.Entity<Teacher>()
                .ToTable("Teacher");  // Use the correct table name as 'dbo.Teacher'

            // Configure the table name for the Student entity if needed
            modelBuilder.Entity<Student>()
                .ToTable("Student");  // Use the correct table name as 'dbo.Student'
        }
    }
}
