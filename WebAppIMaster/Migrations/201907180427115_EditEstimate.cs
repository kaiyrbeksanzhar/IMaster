namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditEstimate : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.RateHowItWorks");
            AddColumn("dbo.RateHowItWorks", "PopulationQuestionId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.RateHowItWorks", new[] { "UserId", "PopulationQuestionId" });
            CreateIndex("dbo.RateHowItWorks", "PopulationQuestionId");
            AddForeignKey("dbo.RateHowItWorks", "PopulationQuestionId", "dbo.PopulationQuestions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RateHowItWorks", "PopulationQuestionId", "dbo.PopulationQuestions");
            DropIndex("dbo.RateHowItWorks", new[] { "PopulationQuestionId" });
            DropPrimaryKey("dbo.RateHowItWorks");
            DropColumn("dbo.RateHowItWorks", "PopulationQuestionId");
            AddPrimaryKey("dbo.RateHowItWorks", new[] { "UserId", "Estimate" });
        }
    }
}
