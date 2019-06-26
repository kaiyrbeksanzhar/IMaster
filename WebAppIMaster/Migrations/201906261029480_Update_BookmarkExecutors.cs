namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_BookmarkExecutors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookmarkExecutors",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ExecutorId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.ExecutorId })
                .ForeignKey("dbo.Executors", t => t.ExecutorId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ExecutorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookmarkExecutors", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookmarkExecutors", "ExecutorId", "dbo.Executors");
            DropIndex("dbo.BookmarkExecutors", new[] { "ExecutorId" });
            DropIndex("dbo.BookmarkExecutors", new[] { "UserId" });
            DropTable("dbo.BookmarkExecutors");
        }
    }
}
