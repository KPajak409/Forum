using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Models;

namespace Forum.Entities
{
    public class ForumDbContext : IdentityDbContext<User, Role, int>
    {
        private readonly string _connectionString = 
            "Server=(localdb)\\mssqllocaldb;Database=ForumDb;Trusted_Connection=True;";

        //public virtual DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<BlackList> BlackList { get; set; }
        public DbSet<Response> Responses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            /*modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(20);*/

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.DateOfBirth)
                .IsRequired();

            modelBuilder.Entity<Topic>()
                .Property(t => t.Content)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Response>()
                .Property(r => r.Content)
                .IsRequired();

            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(40);

            modelBuilder.Entity<Category>()
                .Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Forum.Models.CreateOrUpdateCategoryDto> CreateCategoryDto { get; set; }

        public DbSet<Forum.Models.CreateTopicDto> CreateTopicDto { get; set; }
    }
}
