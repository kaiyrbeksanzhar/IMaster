namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulationQuesstionIdtwo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PopulationCategories", "PopulationCategory_Id", "dbo.PopulationCategories");
            DropIndex("dbo.PopulationCategories", new[] { "PopulationCategory_Id" });
            DropColumn("dbo.PopulationCategories", "PopulationCategory_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PopulationCategories", "PopulationCategory_Id", c => c.Int());
            CreateIndex("dbo.PopulationCategories", "PopulationCategory_Id");
            AddForeignKey("dbo.PopulationCategories", "PopulationCategory_Id", "dbo.PopulationCategories", "Id");
        }
    }
}
