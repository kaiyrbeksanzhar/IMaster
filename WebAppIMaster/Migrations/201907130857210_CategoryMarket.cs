namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryMarket : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrganizationCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.OrganizationCategories", new[] { "CategoryId" });
            DropPrimaryKey("dbo.OrganizationCategories");
            AddColumn("dbo.OrganizationCategories", "CategoryMarketId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.OrganizationCategories", new[] { "OrganizationId", "CategoryMarketId" });
            CreateIndex("dbo.OrganizationCategories", "CategoryMarketId");
            AddForeignKey("dbo.OrganizationCategories", "CategoryMarketId", "dbo.CategoryMarkets", "Id", cascadeDelete: true);
            DropColumn("dbo.OrganizationCategories", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrganizationCategories", "CategoryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrganizationCategories", "CategoryMarketId", "dbo.CategoryMarkets");
            DropIndex("dbo.OrganizationCategories", new[] { "CategoryMarketId" });
            DropPrimaryKey("dbo.OrganizationCategories");
            DropColumn("dbo.OrganizationCategories", "CategoryMarketId");
            AddPrimaryKey("dbo.OrganizationCategories", new[] { "OrganizationId", "CategoryId" });
            CreateIndex("dbo.OrganizationCategories", "CategoryId");
            AddForeignKey("dbo.OrganizationCategories", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
