namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UrlPhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "UrlPhoto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "UrlPhoto");
        }
    }
}
