namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulationQuesstionId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PopulationQuestions", "PopulationCategory_Id", "dbo.PopulationCategories");
            DropIndex("dbo.PopulationQuestions", new[] { "PopulationCategory_Id" });
            DropColumn("dbo.PopulationQuestions", "PopulationCategoryId");
            RenameColumn(table: "dbo.PopulationQuestions", name: "PopulationCategory_Id", newName: "PopulationCategoryId");
            AlterColumn("dbo.PopulationQuestions", "PopulationCategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.PopulationQuestions", "PopulationCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.PopulationQuestions", "PopulationCategoryId");
            AddForeignKey("dbo.PopulationQuestions", "PopulationCategoryId", "dbo.PopulationCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PopulationQuestions", "PopulationCategoryId", "dbo.PopulationCategories");
            DropIndex("dbo.PopulationQuestions", new[] { "PopulationCategoryId" });
            AlterColumn("dbo.PopulationQuestions", "PopulationCategoryId", c => c.Int());
            AlterColumn("dbo.PopulationQuestions", "PopulationCategoryId", c => c.String());
            RenameColumn(table: "dbo.PopulationQuestions", name: "PopulationCategoryId", newName: "PopulationCategory_Id");
            AddColumn("dbo.PopulationQuestions", "PopulationCategoryId", c => c.String());
            CreateIndex("dbo.PopulationQuestions", "PopulationCategory_Id");
            AddForeignKey("dbo.PopulationQuestions", "PopulationCategory_Id", "dbo.PopulationCategories", "Id");
        }
    }
}
