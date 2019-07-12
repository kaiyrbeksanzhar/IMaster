namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrganizationCategoryMarketInCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false),
                        CaTegoryMarktId = c.Int(nullable: false),
                        CreatedAt_Date = c.DateTime(nullable: false),
                        CategoryMarket_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.CategoryId, t.CaTegoryMarktId })
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.CategoryMarkets", t => t.CategoryMarket_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.CategoryMarket_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrganizationCategoryMarketInCategories", "CategoryMarket_Id", "dbo.CategoryMarkets");
            DropForeignKey("dbo.OrganizationCategoryMarketInCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.OrganizationCategoryMarketInCategories", new[] { "CategoryMarket_Id" });
            DropIndex("dbo.OrganizationCategoryMarketInCategories", new[] { "CategoryId" });
            DropTable("dbo.OrganizationCategoryMarketInCategories");
        }
    }
}
