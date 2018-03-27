using System.Data.Entity.Migrations;

namespace CinemaSchedule.DataBase.Migrations
{
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cinemas",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Sessions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    BeginDate = c.DateTime(nullable: false),
                    FilmId = c.Int(nullable: false),
                    CinemaId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Films", t => t.FilmId, cascadeDelete: true)
                .ForeignKey("dbo.Cinemas", t => t.CinemaId, cascadeDelete: true)
                .Index(t => t.FilmId)
                .Index(t => t.CinemaId);

            CreateTable(
                "dbo.Films",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Length = c.Time(nullable: false, precision: 7),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "CinemaId", "dbo.Cinemas");
            DropForeignKey("dbo.Sessions", "FilmId", "dbo.Films");
            DropIndex("dbo.Sessions", new[] { "CinemaId" });
            DropIndex("dbo.Sessions", new[] { "FilmId" });
            DropTable("dbo.Films");
            DropTable("dbo.Sessions");
            DropTable("dbo.Cinemas");
        }
    }
}
