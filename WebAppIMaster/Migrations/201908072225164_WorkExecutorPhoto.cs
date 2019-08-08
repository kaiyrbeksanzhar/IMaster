namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkExecutorPhoto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExecutorWorkPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        ExecutorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Executors", t => t.ExecutorId)
                .Index(t => t.ExecutorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExecutorWorkPhotoes", "ExecutorId", "dbo.Executors");
            DropIndex("dbo.ExecutorWorkPhotoes", new[] { "ExecutorId" });
            DropTable("dbo.ExecutorWorkPhotoes");
        }
    }
}
