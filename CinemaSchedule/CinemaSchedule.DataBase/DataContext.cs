using CinemaSchedule.DataBase.Configurations;
using CinemaSchedule.DataBase.Models;
using System;
using System.Data.Entity;

namespace CinemaSchedule.DataBase
{
    public class DataContext : DbContext
    {
        public DataContext() : base("CinemaScheduleDb")
        {
            Initialize();
        }
        public DataContext(String connectionStringValueOrName) : base(connectionStringValueOrName)
        {
            Initialize();
        }


        // ENTITIES ///////////////////////////////////////////////////////////////////////////////
        public DbSet<CinemaDb> Cinemas { get; set; }
        public DbSet<SessionDb> Sessions { get; set; }
        public DbSet<FilmDb> Films { get; set; }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private void Initialize()
        {
            Database.SetInitializer(new DbInitialyzerConfig());
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CinemaDb>().ToTable("Cinemas");
            modelBuilder.Entity<CinemaDb>().HasKey(p => p.Id);
            modelBuilder.Entity<CinemaDb>().HasMany(p => p.Sessions).WithRequired(p => p.Cinema).Map(m => m.MapKey("CinemaId")).WillCascadeOnDelete(true);

            modelBuilder.Entity<SessionDb>().ToTable("Sessions");
            modelBuilder.Entity<SessionDb>().HasKey(p => p.Id);
            modelBuilder.Entity<SessionDb>().HasRequired(p => p.Film).WithMany().Map(m => m.MapKey("FilmId"));

            modelBuilder.Entity<FilmDb>().ToTable("Films");
            modelBuilder.Entity<FilmDb>().HasKey(p => p.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}

// add-migration MigrationName -projectname "CinemaSchedule.DataBase" -startupproject "CinemaSchedule" -verbose
// update-database -startupproject "CinemaSchedule" -project "CinemaSchedule.DataBase" -verbose