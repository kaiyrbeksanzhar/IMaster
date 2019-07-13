namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrganizationCategories",
                c => new
                    {
                        OrganizationId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrganizationId, t.CategoryId })
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrganizationCategories", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.OrganizationCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.OrganizationCategories", new[] { "CategoryId" });
            DropIndex("dbo.OrganizationCategories", new[] { "OrganizationId" });
            DropTable("dbo.OrganizationCategories");
        }
    }
}
