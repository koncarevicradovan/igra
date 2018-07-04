namespace Igra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangetypeofPlayer1andPlayer2inGames : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Game", "Player1", c => c.String());
            AlterColumn("dbo.Game", "Player2", c => c.String());
            AlterColumn("dbo.Game", "Player1Game1Points", c => c.Int());
            AlterColumn("dbo.Game", "Player2Game1Points", c => c.Int());
            AlterColumn("dbo.Game", "Player1Game2Points", c => c.Int());
            AlterColumn("dbo.Game", "Player2Game2Points", c => c.Int());
            AlterColumn("dbo.Game", "Player1Game3Points", c => c.Int());
            AlterColumn("dbo.Game", "Player2Game3Points", c => c.Int());
            AlterColumn("dbo.Game", "Player1Game4Points", c => c.Int());
            AlterColumn("dbo.Game", "Player2Game4Points", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Game", "Player2Game4Points", c => c.Int(nullable: false));
            AlterColumn("dbo.Game", "Player1Game4Points", c => c.Int(nullable: false));
            AlterColumn("dbo.Game", "Player2Game3Points", c => c.Int(nullable: false));
            AlterColumn("dbo.Game", "Player1Game3Points", c => c.Int(nullable: false));
            AlterColumn("dbo.Game", "Player2Game2Points", c => c.Int(nullable: false));
            AlterColumn("dbo.Game", "Player1Game2Points", c => c.Int(nullable: false));
            AlterColumn("dbo.Game", "Player2Game1Points", c => c.Int(nullable: false));
            AlterColumn("dbo.Game", "Player1Game1Points", c => c.Int(nullable: false));
            AlterColumn("dbo.Game", "Player2", c => c.Int(nullable: false));
            AlterColumn("dbo.Game", "Player1", c => c.Int(nullable: false));
        }
    }
}
