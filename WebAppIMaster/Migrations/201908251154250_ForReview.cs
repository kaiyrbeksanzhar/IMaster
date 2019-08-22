namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForReview : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        ReviewText = c.String(),
                        CustomerId = c.String(maxLength: 128),
                        ExecutorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Executors", t => t.ExecutorId)
                .Index(t => t.CustomerId)
                .Index(t => t.ExecutorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "ExecutorId", "dbo.Executors");
            DropForeignKey("dbo.Reviews", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Reviews", new[] { "ExecutorId" });
            DropIndex("dbo.Reviews", new[] { "CustomerId" });
            DropTable("dbo.Reviews");
        }
    }
}
