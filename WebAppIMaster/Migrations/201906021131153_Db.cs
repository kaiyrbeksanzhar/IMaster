namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CustomerOrders", new[] { "Executor_Id" });
            DropIndex("dbo.CustomerOrders", new[] { "Customer_Id" });
            DropIndex("dbo.ExecutorPhotoFiles", new[] { "Executor_Id" });
            DropColumn("dbo.CustomerOrders", "CustomerId");
            DropColumn("dbo.CustomerOrders", "ExecutorId");
            DropColumn("dbo.ExecutorPhotoFiles", "ExecutorId");
            RenameColumn(table: "dbo.CustomerOrders", name: "CategoryAndSpecializationId", newName: "CategoryId");
            RenameColumn(table: "dbo.CustomerOrders", name: "Customer_Id", newName: "CustomerId");
            RenameColumn(table: "dbo.CustomerOrders", name: "Executor_Id", newName: "ExecutorId");
            RenameColumn(table: "dbo.ExecutorPhotoFiles", name: "Executor_Id", newName: "ExecutorId");
            RenameIndex(table: "dbo.CustomerOrders", name: "IX_CategoryAndSpecializationId", newName: "IX_CategoryId");
            DropPrimaryKey("dbo.OrderExecutors");
            CreateTable(
                "dbo.CallToClients",
                c => new
                    {
                        ExecutorId = c.String(nullable: false, maxLength: 128),
                        OrderExecutorId = c.Int(nullable: false),
                        CreatedAt_Date = c.DateTime(nullable: false),
                        OrderExecutor_OrderId = c.Int(),
                        OrderExecutor_CustomerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ExecutorId, t.OrderExecutorId })
                .ForeignKey("dbo.Executors", t => t.ExecutorId, cascadeDelete: true)
                .ForeignKey("dbo.OrderExecutors", t => new { t.OrderExecutor_OrderId, t.OrderExecutor_CustomerId })
                .Index(t => t.ExecutorId)
                .Index(t => new { t.OrderExecutor_OrderId, t.OrderExecutor_CustomerId });
            
            CreateTable(
                "dbo.CancelOrders",
                c => new
                    {
                        ExecutorId = c.String(nullable: false, maxLength: 128),
                        OrderExecutorId = c.Int(nullable: false),
                        cancelType = c.Int(nullable: false),
                        CanceledDateTime = c.DateTime(nullable: false),
                        OrderExecutor_OrderId = c.Int(),
                        OrderExecutor_CustomerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ExecutorId, t.OrderExecutorId })
                .ForeignKey("dbo.Executors", t => t.ExecutorId, cascadeDelete: true)
                .ForeignKey("dbo.OrderExecutors", t => new { t.OrderExecutor_OrderId, t.OrderExecutor_CustomerId })
                .Index(t => t.ExecutorId)
                .Index(t => new { t.OrderExecutor_OrderId, t.OrderExecutor_CustomerId });
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        ExecutorId = c.String(nullable: false, maxLength: 128),
                        OrderExecutorId = c.Int(nullable: false),
                        CreatedAt_Date = c.DateTime(nullable: false),
                        responseComment = c.String(),
                        OrderExecutor_OrderId = c.Int(),
                        OrderExecutor_CustomerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ExecutorId, t.OrderExecutorId })
                .ForeignKey("dbo.Executors", t => t.ExecutorId, cascadeDelete: true)
                .ForeignKey("dbo.OrderExecutors", t => new { t.OrderExecutor_OrderId, t.OrderExecutor_CustomerId })
                .Index(t => t.ExecutorId)
                .Index(t => new { t.OrderExecutor_OrderId, t.OrderExecutor_CustomerId });
            
            AddColumn("dbo.Executors", "AvatarUrl", c => c.String());
            AlterColumn("dbo.CustomerOrders", "CustomerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.CustomerOrders", "ExecutorId", c => c.String(maxLength: 128));
            AlterColumn("dbo.ExecutorPhotoFiles", "ExecutorId", c => c.String(maxLength: 128));
            AlterColumn("dbo.OrderExecutors", "CustomerId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.OrderExecutors", new[] { "OrderId", "CustomerId" });
            CreateIndex("dbo.CustomerOrders", "CustomerId");
            CreateIndex("dbo.CustomerOrders", "ExecutorId");
            CreateIndex("dbo.ExecutorPhotoFiles", "ExecutorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responses", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" }, "dbo.OrderExecutors");
            DropForeignKey("dbo.Responses", "ExecutorId", "dbo.Executors");
            DropForeignKey("dbo.CancelOrders", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" }, "dbo.OrderExecutors");
            DropForeignKey("dbo.CancelOrders", "ExecutorId", "dbo.Executors");
            DropForeignKey("dbo.CallToClients", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" }, "dbo.OrderExecutors");
            DropForeignKey("dbo.CallToClients", "ExecutorId", "dbo.Executors");
            DropIndex("dbo.Responses", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" });
            DropIndex("dbo.Responses", new[] { "ExecutorId" });
            DropIndex("dbo.CancelOrders", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" });
            DropIndex("dbo.CancelOrders", new[] { "ExecutorId" });
            DropIndex("dbo.ExecutorPhotoFiles", new[] { "ExecutorId" });
            DropIndex("dbo.CustomerOrders", new[] { "ExecutorId" });
            DropIndex("dbo.CustomerOrders", new[] { "CustomerId" });
            DropIndex("dbo.CallToClients", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" });
            DropIndex("dbo.CallToClients", new[] { "ExecutorId" });
            DropPrimaryKey("dbo.OrderExecutors");
            AlterColumn("dbo.OrderExecutors", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.ExecutorPhotoFiles", "ExecutorId", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomerOrders", "ExecutorId", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomerOrders", "CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.Executors", "AvatarUrl");
            DropTable("dbo.Responses");
            DropTable("dbo.CancelOrders");
            DropTable("dbo.CallToClients");
            AddPrimaryKey("dbo.OrderExecutors", new[] { "OrderId", "CustomerId" });
            RenameIndex(table: "dbo.CustomerOrders", name: "IX_CategoryId", newName: "IX_CategoryAndSpecializationId");
            RenameColumn(table: "dbo.ExecutorPhotoFiles", name: "ExecutorId", newName: "Executor_Id");
            RenameColumn(table: "dbo.CustomerOrders", name: "ExecutorId", newName: "Executor_Id");
            RenameColumn(table: "dbo.CustomerOrders", name: "CustomerId", newName: "Customer_Id");
            RenameColumn(table: "dbo.CustomerOrders", name: "CategoryId", newName: "CategoryAndSpecializationId");
            AddColumn("dbo.ExecutorPhotoFiles", "ExecutorId", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerOrders", "ExecutorId", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerOrders", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExecutorPhotoFiles", "Executor_Id");
            CreateIndex("dbo.CustomerOrders", "Customer_Id");
            CreateIndex("dbo.CustomerOrders", "Executor_Id");
        }
    }
}
