namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationIP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryMarkets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Organization_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .Index(t => t.Organization_Id);
            
            CreateTable(
                "dbo.CategoryMarketLangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Langcode = c.String(),
                        CategoryMarketId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryMarkets", t => t.CategoryMarketId, cascadeDelete: true)
                .Index(t => t.CategoryMarketId);
            
            AddColumn("dbo.Customers", "AvatarUrl", c => c.String());
            AddColumn("dbo.OrganizationPromotionAndDiscounts", "Priority", c => c.Int(nullable: false));
            AddColumn("dbo.Supports", "FileUrl", c => c.String());
            DropColumn("dbo.Organizations", "MarketCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Organizations", "MarketCategory", c => c.String());
            DropForeignKey("dbo.CategoryMarkets", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.CategoryMarketLangs", "CategoryMarketId", "dbo.CategoryMarkets");
            DropIndex("dbo.CategoryMarketLangs", new[] { "CategoryMarketId" });
            DropIndex("dbo.CategoryMarkets", new[] { "Organization_Id" });
            DropColumn("dbo.Supports", "FileUrl");
            DropColumn("dbo.OrganizationPromotionAndDiscounts", "Priority");
            DropColumn("dbo.Customers", "AvatarUrl");
            DropTable("dbo.CategoryMarketLangs");
            DropTable("dbo.CategoryMarkets");
        }
    }
}
