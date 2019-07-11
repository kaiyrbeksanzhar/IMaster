namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbEditto125 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "TarifType", c => c.Int(nullable: false));
            DropColumn("dbo.OrganizationLangs", "BannerUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrganizationLangs", "BannerUrl", c => c.String());
            DropColumn("dbo.Organizations", "TarifType");
        }
    }
}
