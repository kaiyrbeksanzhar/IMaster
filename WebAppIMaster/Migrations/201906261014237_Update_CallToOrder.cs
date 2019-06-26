namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_CallToOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CallToClients", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" }, "dbo.OrderExecutors");
            DropIndex("dbo.CallToClients", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" });
            DropPrimaryKey("dbo.CallToClients");
            AddColumn("dbo.CallToClients", "OrderId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CallToClients", new[] { "ExecutorId", "OrderId" });
            CreateIndex("dbo.CallToClients", "OrderId");
            AddForeignKey("dbo.CallToClients", "OrderId", "dbo.CustomerOrders", "Id", cascadeDelete: false);
            DropColumn("dbo.CallToClients", "OrderExecutorId");
            DropColumn("dbo.CallToClients", "OrderExecutor_OrderId");
            DropColumn("dbo.CallToClients", "OrderExecutor_CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CallToClients", "OrderExecutor_CustomerId", c => c.String(maxLength: 128));
            AddColumn("dbo.CallToClients", "OrderExecutor_OrderId", c => c.Int());
            AddColumn("dbo.CallToClients", "OrderExecutorId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CallToClients", "OrderId", "dbo.CustomerOrders");
            DropIndex("dbo.CallToClients", new[] { "OrderId" });
            DropPrimaryKey("dbo.CallToClients");
            DropColumn("dbo.CallToClients", "OrderId");
            AddPrimaryKey("dbo.CallToClients", new[] { "ExecutorId", "OrderExecutorId" });
            CreateIndex("dbo.CallToClients", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" });
            AddForeignKey("dbo.CallToClients", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" }, "dbo.OrderExecutors", new[] { "OrderId", "CustomerId" });
        }
    }
}
