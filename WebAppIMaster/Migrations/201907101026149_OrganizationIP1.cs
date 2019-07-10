namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationIP1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "YouTubeVideoUrlkz", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "YouTubeVideoUrlkz");
        }
    }
}
