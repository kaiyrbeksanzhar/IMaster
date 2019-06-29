namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionPopulation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PopulationCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Property = c.Int(nullable: false),
                        PopulationCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PopulationCategories", t => t.PopulationCategory_Id)
                .Index(t => t.PopulationCategory_Id);
            
            CreateTable(
                "dbo.PopulationCategoryLangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LangCode = c.String(),
                        PopulationCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PopulationCategories", t => t.PopulationCategoryId, cascadeDelete: true)
                .Index(t => t.PopulationCategoryId);
            
            CreateTable(
                "dbo.PopulationQuestionLangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        LangCode = c.String(),
                        PopulationQuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PopulationQuestions", t => t.PopulationQuestionId, cascadeDelete: true)
                .Index(t => t.PopulationQuestionId);
            
            CreateTable(
                "dbo.PopulationQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PopulationCategoryId = c.String(),
                        PopulationCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PopulationCategories", t => t.PopulationCategory_Id)
                .Index(t => t.PopulationCategory_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PopulationQuestions", "PopulationCategory_Id", "dbo.PopulationCategories");
            DropForeignKey("dbo.PopulationQuestionLangs", "PopulationQuestionId", "dbo.PopulationQuestions");
            DropForeignKey("dbo.PopulationCategories", "PopulationCategory_Id", "dbo.PopulationCategories");
            DropForeignKey("dbo.PopulationCategoryLangs", "PopulationCategoryId", "dbo.PopulationCategories");
            DropIndex("dbo.PopulationQuestions", new[] { "PopulationCategory_Id" });
            DropIndex("dbo.PopulationQuestionLangs", new[] { "PopulationQuestionId" });
            DropIndex("dbo.PopulationCategoryLangs", new[] { "PopulationCategoryId" });
            DropIndex("dbo.PopulationCategories", new[] { "PopulationCategory_Id" });
            DropTable("dbo.PopulationQuestions");
            DropTable("dbo.PopulationQuestionLangs");
            DropTable("dbo.PopulationCategoryLangs");
            DropTable("dbo.PopulationCategories");
        }
    }
}
