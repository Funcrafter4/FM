using System;
using Findmaster.DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace Findmaster.DataAccessLayer.DataContext
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<User_Info> Users_Info { get; set; }
        public DbSet<User_Type> Users_Type { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Applications> Applications { get; set; }

        public DatabaseContext() => Database.EnsureCreated();
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserInfo
            modelBuilder.Entity<User_Info>().HasKey(ui => new { ui.UserId});
            #endregion
            #region UserType
            modelBuilder.Entity<User_Type>().HasKey(ut => new { ut.UserId });
            
            #endregion

            #region Messages

            modelBuilder.Entity<Messages>().Property(ms => ms.Message).IsRequired(true).HasMaxLength(1000).HasColumnName("message_text");

            #endregion
            #region User
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(u => u.UserEmail).IsRequired(true).HasMaxLength(100).HasColumnName("user_email");
            #endregion
            #region Vacancy

            modelBuilder.Entity<Vacancy>().ToTable("vacancies");
            //Key Auto
            modelBuilder.Entity<Vacancy>().Property(vc => vc.VacancyName).IsRequired(true).HasMaxLength(100).HasColumnName("vacancy_name");
            modelBuilder.Entity<Vacancy>().Property(vc => vc.VacancyAddress).IsRequired(true).HasMaxLength(100).HasColumnName("vacancy_address");
            modelBuilder.Entity<Vacancy>().Property(vc => vc.VacancyEmployerName).IsRequired(true).HasMaxLength(100).HasColumnName("vacancy_employername");
            modelBuilder.Entity<Vacancy>().Property(vc => vc.VacancyRequirements).IsRequired(true).HasMaxLength(10000).HasColumnName("vacancy_requirements");
            modelBuilder.Entity<Vacancy>().Property(vc => vc.VacancyDescription).IsRequired(true).HasMaxLength(10000).HasColumnName("vacancy_description");
            modelBuilder.Entity<Vacancy>().Property(vc => vc.VacancyExp).IsRequired(true).HasMaxLength(100).HasColumnName("vacancy_experience");
            modelBuilder.Entity<Vacancy>().Property(vc => vc.VacancyEmploymentType).IsRequired(true).HasMaxLength(100).HasColumnName("vacancy_employment_type");
            #endregion

            // использование Fluent API
            base.OnModelCreating(modelBuilder);
        }
    }

}
