using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class SkillManagementContext : IdentityDbContext<IdentityUser>
    {
        public SkillManagementContext(DbContextOptions<SkillManagementContext> options) : base(options)
        {
        }

        public UserManager<IdentityUser> UserManager { get; }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship
            modelBuilder.Entity<UserSkill>()
                .HasKey(us => new { us.UserId, us.SkillId });

            modelBuilder.Entity<UserSkill>()
                .HasOne(us => us.User);

            modelBuilder.Entity<UserSkill>()
                .Navigation(us => us.Skill)
                .AutoInclude();

            // Apply AutoInclude to Skills navigation property
            modelBuilder.Entity<User>()
                .Navigation(u => u.UserSkills)
                .AutoInclude();

            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "C#" },
                new Skill { Id = 2, Name = "SQL" },
                new Skill { Id = 3, Name = "Azure" }
            );
        }
    }
}
