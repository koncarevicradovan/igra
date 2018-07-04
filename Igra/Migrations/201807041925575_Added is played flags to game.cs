namespace Igra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedisplayedflagstogame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Game", "Player1Game1Played", c => c.Boolean(nullable: false));
            AddColumn("dbo.Game", "Player2Game1Played", c => c.Boolean(nullable: false));
            AddColumn("dbo.Game", "Player1Game2Played", c => c.Boolean(nullable: false));
            AddColumn("dbo.Game", "Player2Game2Played", c => c.Boolean(nullable: false));
            AddColumn("dbo.Game", "Player1Game3Played", c => c.Boolean(nullable: false));
            AddColumn("dbo.Game", "Player2Game3Played", c => c.Boolean(nullable: false));
            AddColumn("dbo.Game", "Player1Game4Played", c => c.Boolean(nullable: false));
            AddColumn("dbo.Game", "Player2Game4Played", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Game", "Player2Game4Played");
            DropColumn("dbo.Game", "Player1Game4Played");
            DropColumn("dbo.Game", "Player2Game3Played");
            DropColumn("dbo.Game", "Player1Game3Played");
            DropColumn("dbo.Game", "Player2Game2Played");
            DropColumn("dbo.Game", "Player1Game2Played");
            DropColumn("dbo.Game", "Player2Game1Played");
            DropColumn("dbo.Game", "Player1Game1Played");
        }
    }
}
