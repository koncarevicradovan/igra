namespace Igra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedmorequestions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GamingUser", "Question11", c => c.String());
            AddColumn("dbo.GamingUser", "Question12", c => c.String());
            AddColumn("dbo.GamingUser", "Question13", c => c.String());
            AddColumn("dbo.GamingUser", "Question14", c => c.String());
            AddColumn("dbo.GamingUser", "Question15", c => c.String());
            AddColumn("dbo.GamingUser", "Question16", c => c.String());
            AddColumn("dbo.GamingUser", "Question17", c => c.String());
            AddColumn("dbo.GamingUser", "Question18", c => c.String());
            AddColumn("dbo.GamingUser", "Question19", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GamingUser", "Question19");
            DropColumn("dbo.GamingUser", "Question18");
            DropColumn("dbo.GamingUser", "Question17");
            DropColumn("dbo.GamingUser", "Question16");
            DropColumn("dbo.GamingUser", "Question15");
            DropColumn("dbo.GamingUser", "Question14");
            DropColumn("dbo.GamingUser", "Question13");
            DropColumn("dbo.GamingUser", "Question12");
            DropColumn("dbo.GamingUser", "Question11");
        }
    }
}
