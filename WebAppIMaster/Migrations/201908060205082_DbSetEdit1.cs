namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbSetEdit1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ExecutorPassportFiles", new[] { "Executor_Id" });
            DropColumn("dbo.ExecutorPassportFiles", "ExecutorId");
            RenameColumn(table: "dbo.ExecutorPassportFiles", name: "Executor_Id", newName: "ExecutorId");
            AlterColumn("dbo.ExecutorPassportFiles", "ExecutorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ExecutorPassportFiles", "ExecutorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ExecutorPassportFiles", new[] { "ExecutorId" });
            AlterColumn("dbo.ExecutorPassportFiles", "ExecutorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ExecutorPassportFiles", name: "ExecutorId", newName: "Executor_Id");
            AddColumn("dbo.ExecutorPassportFiles", "ExecutorId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExecutorPassportFiles", "Executor_Id");
        }
    }
}
