namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Estimate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RateHowItWorks",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Estimate = c.Int(nullable: false),
                        CreatedAt_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.Estimate });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RateHowItWorks");
        }
    }
}
