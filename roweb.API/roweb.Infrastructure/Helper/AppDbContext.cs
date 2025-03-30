using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using roweb.Domain.Helper;
using roweb.Domain.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace roweb.Infrastructure
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();
        }
    }
}
