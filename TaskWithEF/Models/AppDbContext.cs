using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Hobbie> Hobbies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// To But DataAnnotation On Field Using FluentApi
            //modelBuilder.Entity<User>().Property(x => x.BirthDate).IsRequired();
            //// To Change In ReferentialAction Using FluentApi
            //modelBuilder.Entity<Hobbie>().
            //    HasOne(x => x.User)
            //    .WithOne(s => s.Hobbies)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
