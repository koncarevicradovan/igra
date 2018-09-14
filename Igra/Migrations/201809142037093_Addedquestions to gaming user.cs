namespace Igra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedquestionstogaminguser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GamingUser", "Question1", c => c.String());
            AddColumn("dbo.GamingUser", "Question2", c => c.String());
            AddColumn("dbo.GamingUser", "Question3", c => c.String());
            AddColumn("dbo.GamingUser", "Question4", c => c.String());
            AddColumn("dbo.GamingUser", "Question5", c => c.String());
            AddColumn("dbo.GamingUser", "Question6", c => c.String());
            AddColumn("dbo.GamingUser", "Question7", c => c.String());
            AddColumn("dbo.GamingUser", "Question8", c => c.String());
            AddColumn("dbo.GamingUser", "Question9", c => c.String());
            AddColumn("dbo.GamingUser", "Question10", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GamingUser", "Question10");
            DropColumn("dbo.GamingUser", "Question9");
            DropColumn("dbo.GamingUser", "Question8");
            DropColumn("dbo.GamingUser", "Question7");
            DropColumn("dbo.GamingUser", "Question6");
            DropColumn("dbo.GamingUser", "Question5");
            DropColumn("dbo.GamingUser", "Question4");
            DropColumn("dbo.GamingUser", "Question3");
            DropColumn("dbo.GamingUser", "Question2");
            DropColumn("dbo.GamingUser", "Question1");
        }
    }
}
