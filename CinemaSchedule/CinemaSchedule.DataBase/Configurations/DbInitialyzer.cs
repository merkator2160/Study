using System.Data.Entity;

namespace CinemaSchedule.DataBase.Configurations
{
    // DropCreateDatabaseAlways<DataContext>
    // DropCreateDatabaseIfModelChanges<DataContext>
    // MigrateDatabaseToLatestVersion<DataContext, Configuration>
    // CreateDatabaseIfNotExists<DataContext>
    public sealed class DbInitialyzerConfig : MigrateDatabaseToLatestVersion<DataContext, MigrationConfig>
    {

    }
}