namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WarningCategoryMarket : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrganizationCategoryMarketInCategories", "CategoryMarket_Id", "dbo.CategoryMarkets");
            DropIndex("dbo.OrganizationCategoryMarketInCategories", new[] { "CategoryMarket_Id" });
            RenameColumn(table: "dbo.OrganizationCategoryMarketInCategories", name: "CategoryMarket_Id", newName: "CategoryMarketId");
            DropPrimaryKey("dbo.OrganizationCategoryMarketInCategories");
            AlterColumn("dbo.OrganizationCategoryMarketInCategories", "CategoryMarketId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.OrganizationCategoryMarketInCategories", new[] { "CategoryId", "CategoryMarketId" });
            CreateIndex("dbo.OrganizationCategoryMarketInCategories", "CategoryMarketId");
            AddForeignKey("dbo.OrganizationCategoryMarketInCategories", "CategoryMarketId", "dbo.CategoryMarkets", "Id", cascadeDelete: true);
            DropColumn("dbo.OrganizationCategoryMarketInCategories", "CaTegoryMarktId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrganizationCategoryMarketInCategories", "CaTegoryMarktId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrganizationCategoryMarketInCategories", "CategoryMarketId", "dbo.CategoryMarkets");
            DropIndex("dbo.OrganizationCategoryMarketInCategories", new[] { "CategoryMarketId" });
            DropPrimaryKey("dbo.OrganizationCategoryMarketInCategories");
            AlterColumn("dbo.OrganizationCategoryMarketInCategories", "CategoryMarketId", c => c.Int());
            AddPrimaryKey("dbo.OrganizationCategoryMarketInCategories", new[] { "CategoryId", "CaTegoryMarktId" });
            RenameColumn(table: "dbo.OrganizationCategoryMarketInCategories", name: "CategoryMarketId", newName: "CategoryMarket_Id");
            CreateIndex("dbo.OrganizationCategoryMarketInCategories", "CategoryMarket_Id");
            AddForeignKey("dbo.OrganizationCategoryMarketInCategories", "CategoryMarket_Id", "dbo.CategoryMarkets", "Id");
        }
    }
}
