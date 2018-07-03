namespace Igra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGamingUserandREadyForGame : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Player1 = c.Int(nullable: false),
                        Player2 = c.Int(nullable: false),
                        Player1Game1Points = c.Int(nullable: false),
                        Player2Game1Points = c.Int(nullable: false),
                        Player1Game2Points = c.Int(nullable: false),
                        Player2Game2Points = c.Int(nullable: false),
                        Player1Game3Points = c.Int(nullable: false),
                        Player2Game3Points = c.Int(nullable: false),
                        Player1Game4Points = c.Int(nullable: false),
                        Player2Game4Points = c.Int(nullable: false),
                        PlayerWinner = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReadyForGame",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Accepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReadyForGame");
            DropTable("dbo.Game");
        }
    }
}
