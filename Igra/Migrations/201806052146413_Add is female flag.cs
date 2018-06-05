namespace Igra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addisfemaleflag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GamingUser", "IsFemale", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GamingUser", "IsFemale");
        }
    }
}
