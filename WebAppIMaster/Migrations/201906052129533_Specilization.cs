namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Specilization : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExecutorCategoryAndSpecializations", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.ExecutorCategoryAndSpecializations", "ExecutorId", "dbo.Executors");
            DropIndex("dbo.ExecutorCategoryAndSpecializations", new[] { "ExecutorId" });
            DropIndex("dbo.ExecutorCategoryAndSpecializations", new[] { "CategoryId" });
            CreateTable(
                "dbo.ExecutorSpecializations",
                c => new
                    {
                        ExecutorId = c.String(nullable: false, maxLength: 128),
                        SpecializationId = c.Int(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.ExecutorId, t.SpecializationId })
                .ForeignKey("dbo.Executors", t => t.ExecutorId, cascadeDelete: true)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.ExecutorId)
                .Index(t => t.SpecializationId)
                .Index(t => t.Category_Id);
            
            DropTable("dbo.ExecutorCategoryAndSpecializations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExecutorCategoryAndSpecializations",
                c => new
                    {
                        ExecutorId = c.String(nullable: false, maxLength: 128),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExecutorId, t.CategoryId });
            
            DropForeignKey("dbo.ExecutorSpecializations", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.ExecutorSpecializations", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.ExecutorSpecializations", "ExecutorId", "dbo.Executors");
            DropIndex("dbo.ExecutorSpecializations", new[] { "Category_Id" });
            DropIndex("dbo.ExecutorSpecializations", new[] { "SpecializationId" });
            DropIndex("dbo.ExecutorSpecializations", new[] { "ExecutorId" });
            DropTable("dbo.ExecutorSpecializations");
            CreateIndex("dbo.ExecutorCategoryAndSpecializations", "CategoryId");
            CreateIndex("dbo.ExecutorCategoryAndSpecializations", "ExecutorId");
            AddForeignKey("dbo.ExecutorCategoryAndSpecializations", "ExecutorId", "dbo.Executors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ExecutorCategoryAndSpecializations", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
