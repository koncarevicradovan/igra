namespace Igra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedfifthgame : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FifthGame",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Player1 = c.String(),
                        Player2 = c.String(),
                        Player1Game1Points = c.Int(),
                        Player2Game1Points = c.Int(),
                        Player1Game2Points = c.Int(),
                        Player2Game2Points = c.Int(),
                        Player1Game3Points = c.Int(),
                        Player2Game3Points = c.Int(),
                        Player1Game4Points = c.Int(),
                        Player2Game4Points = c.Int(),
                        Player1Game5Points = c.Int(),
                        Player2Game5Points = c.Int(),
                        Player1Game1Played = c.Boolean(nullable: false),
                        Player2Game1Played = c.Boolean(nullable: false),
                        Player1Game2Played = c.Boolean(nullable: false),
                        Player2Game2Played = c.Boolean(nullable: false),
                        Player1Game3Played = c.Boolean(nullable: false),
                        Player2Game3Played = c.Boolean(nullable: false),
                        Player1Game4Played = c.Boolean(nullable: false),
                        Player2Game4Played = c.Boolean(nullable: false),
                        Player1Game5Played = c.Boolean(nullable: false),
                        Player2Game5Played = c.Boolean(nullable: false),
                        PlayerWinner = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FifthGame");
        }
    }
}
