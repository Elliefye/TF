using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TF2.Entities
{
    class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<LecturersAndSubjects> LecturersAndSubjects { get; set; }
        public DbSet<LecturerReview> LecturerReviews { get; set; }
        public DbSet<SubjectReview> SubjectReviews { get; set; }
 
        private string DbPath;

        public Context(string dbpath)
        {
            DbPath = dbpath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={DbPath}");
        }
    }
}
