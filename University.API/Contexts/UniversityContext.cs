using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using University.API.Entities;

namespace University.API.Contexts
{
    public class UniversityContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasData(
                new Course()
                {
                    Id = 1,
                    Name = "C# Starter",
                    Description = "Start learn cool language"
                },
                new Course()
                {
                    Id = 2,
                    Name = "Angular 9",
                    Description = "For angular beginners"
                });

            modelBuilder.Entity<Lesson>()
                .HasData(
                 new Lesson()
                 {
                     Id = 1,
                     CourseId = 1,
                     Name = "First",
                     Description = "Intro",
                 },
                 new Lesson()
                 {
                     Id = 2,
                     CourseId = 1,
                     Name = "Second",
                     Description = "Data types"
                 },
                 new Lesson()
                 {
                     Id = 3,
                     CourseId = 2,
                     Name = "First",
                     Description = "Intro"
                 },
                 new Lesson()
                 {
                     Id = 4,
                     CourseId = 2,
                     Name = "Second",
                     Description = "Data types"
                 }
            );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
    }
}
