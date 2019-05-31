namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddExecutorToOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(),
                        OrderId = c.Int(nullable: false),
                        ExecutorId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CancelExecutorResponses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(),
                        OrderId = c.Int(nullable: false),
                        ExecutorId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CustomerOrders", "OrderNumber", c => c.String());
            AddColumn("dbo.CustomerOrders", "NewNotifications", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerOrders", "PayWithBounce", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "Bonus", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Customers", "InCityId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "FatherName", c => c.String());
            AddColumn("dbo.AspNetUsers", "GenderId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "GenderName", c => c.String());
            AddColumn("dbo.Executors", "ExecutorCheck", c => c.Boolean(nullable: false));
            AddColumn("dbo.Executors", "CityId", c => c.Int(nullable: false));
            AddColumn("dbo.Executors", "ExecotorOnline", c => c.Boolean(nullable: false));
            AddColumn("dbo.Executors", "ExecutorClosedOrdersCount", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "InCityId");
            CreateIndex("dbo.Executors", "CityId");
            AddForeignKey("dbo.Customers", "InCityId", "dbo.Cities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Executors", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Executors", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Customers", "InCityId", "dbo.Cities");
            DropIndex("dbo.Executors", new[] { "CityId" });
            DropIndex("dbo.Customers", new[] { "InCityId" });
            DropColumn("dbo.Executors", "ExecutorClosedOrdersCount");
            DropColumn("dbo.Executors", "ExecotorOnline");
            DropColumn("dbo.Executors", "CityId");
            DropColumn("dbo.Executors", "ExecutorCheck");
            DropColumn("dbo.AspNetUsers", "GenderName");
            DropColumn("dbo.AspNetUsers", "GenderId");
            DropColumn("dbo.AspNetUsers", "FatherName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.Customers", "InCityId");
            DropColumn("dbo.Customers", "Bonus");
            DropColumn("dbo.CustomerOrders", "PayWithBounce");
            DropColumn("dbo.CustomerOrders", "NewNotifications");
            DropColumn("dbo.CustomerOrders", "OrderNumber");
            DropTable("dbo.CancelExecutorResponses");
            DropTable("dbo.AddExecutorToOrders");
        }
    }
}
