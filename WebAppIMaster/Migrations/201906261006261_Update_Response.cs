namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Response : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Responses", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" }, "dbo.OrderExecutors");
            DropIndex("dbo.Responses", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" });
            DropPrimaryKey("dbo.Responses");
            AddColumn("dbo.Responses", "OrderId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Responses", new[] { "ExecutorId", "OrderId" });
            CreateIndex("dbo.Responses", "OrderId");
            AddForeignKey("dbo.Responses", "OrderId", "dbo.CustomerOrders", "Id", cascadeDelete: false);
            DropColumn("dbo.Responses", "OrderExecutorId");
            DropColumn("dbo.Responses", "OrderExecutor_OrderId");
            DropColumn("dbo.Responses", "OrderExecutor_CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Responses", "OrderExecutor_CustomerId", c => c.String(maxLength: 128));
            AddColumn("dbo.Responses", "OrderExecutor_OrderId", c => c.Int());
            AddColumn("dbo.Responses", "OrderExecutorId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Responses", "OrderId", "dbo.CustomerOrders");
            DropIndex("dbo.Responses", new[] { "OrderId" });
            DropPrimaryKey("dbo.Responses");
            DropColumn("dbo.Responses", "OrderId");
            AddPrimaryKey("dbo.Responses", new[] { "ExecutorId", "OrderExecutorId" });
            CreateIndex("dbo.Responses", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" });
            AddForeignKey("dbo.Responses", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" }, "dbo.OrderExecutors", new[] { "OrderId", "CustomerId" });
        }
    }
}
