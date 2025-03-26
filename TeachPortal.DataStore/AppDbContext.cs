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
    }
}
