using EF_core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF_core.Contexts
{
    public class AppDbContext : DbContext
    {
        private readonly string SQLInstance = null!;
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Department> Departments { get; set; }

        public AppDbContext()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            SQLInstance = config.GetConnectionString("DefaultConnection")!;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(SQLInstance);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Teacher - Subject (many-to-many)
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Subjects)
                .WithMany(s => s.Teachers)
                .UsingEntity(j => j
                    .ToTable("SubjectTeacher"));

            // Teacher - Group (many-to-many)
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Groups)
                .WithMany(g => g.Teachers)
                .UsingEntity(j => j
                    .ToTable("GroupTeacher"));

            // Disable cascade on Department relationships
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Department)
                .WithMany(d => d.Teachers)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Subjects)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
