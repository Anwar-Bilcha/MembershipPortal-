using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MembershipPortal.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MembershipPortal

{
    public class RepositoryContext : IdentityDbContext
    { 
        public RepositoryContext(DbContextOptions options) : base(options)
    { }
        public DbSet<Member> Members { get; set; }
        public DbSet<Publication> Publications { get; set; }



        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Member>().ToTable("Members");
            modelBuilder.Entity<Member>().HasData
(
new Member
{
    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
    firstName = "Anwar",
    middleName = "Bilcha",
    lasttName = "Hussein",
    gender = "Male",
    city = "Adama",
    dateofBirth = new DateTime(10/04/1989),
    academicRank = "MSc.",
    academicTitle = "Lecturer",
    speciality = "Software Engineering",
    fieldofStudy = "Computer Science"
},
new Member
{
    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991987"),
    firstName = "Nebilr",
    middleName = "Nuru",
    lasttName = "Abawari",
    gender = "Male",
    city = "Adama",
    dateofBirth = new DateTime(10/04/1990),
    academicRank = "BSc.",
    academicTitle = "Engineer",
    speciality = "Civil Engineering",
    fieldofStudy = "Civil Engineering",
}
);
        }
    }
}
