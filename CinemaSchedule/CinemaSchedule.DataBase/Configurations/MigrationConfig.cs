using CinemaSchedule.DataBase.Models;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace CinemaSchedule.DataBase.Configurations
{
    public sealed class MigrationConfig : DbMigrationsConfiguration<DataContext>
    {
        public MigrationConfig()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "CinemaSchedule.Database.DataContext";
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        protected override void Seed(DataContext context)
        {
            if (!context.Cinemas.Any())
            {
                var films = AddFilms(context);
                AddCinemas(context, films);
            }
        }   // This method will be called after migrating to the latest version use AddOrUpdate() function to avoid duplicates.
        private FilmDb[] AddFilms(DataContext context)
        {
            return context.Films.AddRange(new FilmDb[]
            {
                new FilmDb()
                {
                    Id = 1,
                    Name = "Дом странных детей мисс Перегрин",
                    Length = TimeSpan.FromMinutes(127)
                },
                new FilmDb()
                {
                    Id = 2,
                    Name = "Дуэлянт",
                    Length = TimeSpan.FromMinutes(110)
                },
                new FilmDb()
                {
                    Id = 3,
                    Name = "Аисты",
                    Length = TimeSpan.FromMinutes(87)
                }
            }).ToArray();
        }
        private void AddCinemas(DataContext context, FilmDb[] films)
        {
            context.Cinemas.AddOrUpdate(p => p.Id, new CinemaDb()
            {
                Id = 1,
                Name = "На калужской",
                Sessions = new SessionDb[]
                {
                    CreateSingleSession(new DateTime(2016, 10, 10, 9, 30, 0), films[2]),
                    CreateSingleSession(new DateTime(2016, 10, 10, 11, 0, 0), films[2]),
                    CreateSingleSession(new DateTime(2016, 10, 10, 12, 30, 0), films[2]),
                    CreateSingleSession(new DateTime(2016, 10, 10, 15, 10, 0), films[2]),
                    CreateSingleSession(new DateTime(2016, 10, 10, 16, 50, 0), films[2]),
                    CreateSingleSession(new DateTime(2016, 10, 10, 18, 30, 0), films[2]),

                    CreateSingleSession(new DateTime(2016, 10, 10, 20, 0, 0), films[0]),
                    CreateSingleSession(new DateTime(2016, 10, 10, 21, 30, 0), films[0]),
                    CreateSingleSession(new DateTime(2016, 10, 10, 23, 0, 0), films[0]),
                }
            });
            context.Cinemas.AddOrUpdate(p => p.Id, new CinemaDb()
            {
                Id = 2,
                Name = "На Октябрьском Поле",
                Sessions = new SessionDb[]
                {
                    CreateSingleSession(new DateTime(2016, 10, 10, 9, 30, 0), films[1]),
                    CreateSingleSession(new DateTime(2016, 10, 10, 11, 0, 0), films[1]),
                    CreateSingleSession(new DateTime(2016, 10, 10, 12, 30, 0), films[1]),
                    CreateSingleSession(new DateTime(2016, 10, 10, 15, 10, 0), films[1]),
                    CreateSingleSession(new DateTime(2016, 10, 10, 16, 50, 0), films[1]),
                    CreateSingleSession(new DateTime(2016, 10, 10, 18, 30, 0), films[1]),
                    CreateSingleSession(new DateTime(2016, 10, 10, 20, 0, 0), films[1]),
                    CreateSingleSession(new DateTime(2016, 10, 10, 21, 30, 0), films[1]),
                    CreateSingleSession(new DateTime(2016, 10, 10, 23, 0, 0), films[1]),
                }
            });
            context.Cinemas.AddOrUpdate(p => p.Id, new CinemaDb()
            {
                Id = 3,
                Name = "На Южной",
                Sessions = new SessionDb[]
                {
                    CreateSingleSession(new DateTime(2016, 10, 10, 10, 30, 0), films[1]),
                    CreateSingleSession(new DateTime(2016, 10, 11, 15, 30, 0), films[2]),
                    CreateSingleSession(new DateTime(2016, 10, 12, 18, 30, 0), films[0]),
                }
            });
            context.Cinemas.AddOrUpdate(p => p.Id, new CinemaDb()
            {
                Id = 4,
                Name = "Филион",
                Sessions = new SessionDb[]
                {
                    CreateSingleSession(new DateTime(2016, 1, 21, 9, 30, 0), films[2]),
                    CreateSingleSession(new DateTime(2016, 12, 7, 12, 45, 0), films[0]),
                    CreateSingleSession(new DateTime(2016, 8, 15, 17, 40, 0), films[1]),
                    CreateSingleSession(new DateTime(2016, 6, 1, 20, 45, 0), films[1]),
                }
            });

            context.SaveChanges();
        }
        private SessionDb CreateSingleSession(DateTime beginDate, FilmDb film)
        {
            return new SessionDb()
            {
                Film = film,
                BeginDate = beginDate
            };
        }
    }
}