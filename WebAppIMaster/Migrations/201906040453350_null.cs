namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _null : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Executors", "CityId", "dbo.Cities");
            DropIndex("dbo.Executors", new[] { "CityId" });
            AlterColumn("dbo.Executors", "BirthDay", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Executors", "Gender", c => c.Int());
            AlterColumn("dbo.Executors", "ExecutorCheck", c => c.Boolean());
            AlterColumn("dbo.Executors", "ExecutorType", c => c.Int());
            AlterColumn("dbo.Executors", "ExecutorStatus", c => c.Int());
            AlterColumn("dbo.Executors", "Rating", c => c.Int());
            AlterColumn("dbo.Executors", "RegistrationDateTime", c => c.DateTime());
            AlterColumn("dbo.Executors", "Banned", c => c.Boolean());
            AlterColumn("dbo.Executors", "BannedDateTime", c => c.DateTime());
            AlterColumn("dbo.Executors", "CityId", c => c.Int());
            AlterColumn("dbo.Executors", "ExecotorOnline", c => c.Boolean());
            AlterColumn("dbo.Executors", "ExecutorClosedOrdersCount", c => c.Int());
            CreateIndex("dbo.Executors", "CityId");
            AddForeignKey("dbo.Executors", "CityId", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Executors", "CityId", "dbo.Cities");
            DropIndex("dbo.Executors", new[] { "CityId" });
            AlterColumn("dbo.Executors", "ExecutorClosedOrdersCount", c => c.Int(nullable: false));
            AlterColumn("dbo.Executors", "ExecotorOnline", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Executors", "CityId", c => c.Int(nullable: false));
            AlterColumn("dbo.Executors", "BannedDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Executors", "Banned", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Executors", "RegistrationDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Executors", "Rating", c => c.Int(nullable: false));
            AlterColumn("dbo.Executors", "ExecutorStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Executors", "ExecutorType", c => c.Int(nullable: false));
            AlterColumn("dbo.Executors", "ExecutorCheck", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Executors", "Gender", c => c.Int(nullable: false));
            AlterColumn("dbo.Executors", "BirthDay", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.Executors", "CityId");
            AddForeignKey("dbo.Executors", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
