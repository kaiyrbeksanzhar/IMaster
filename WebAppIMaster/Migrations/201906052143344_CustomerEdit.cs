namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerEdit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "InCityId", "dbo.Cities");
            DropIndex("dbo.Customers", new[] { "InCityId" });
            AlterColumn("dbo.Customers", "Bonus", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Customers", "Status", c => c.Int());
            AlterColumn("dbo.Customers", "InCityId", c => c.Int());
            CreateIndex("dbo.Customers", "InCityId");
            AddForeignKey("dbo.Customers", "InCityId", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "InCityId", "dbo.Cities");
            DropIndex("dbo.Customers", new[] { "InCityId" });
            AlterColumn("dbo.Customers", "InCityId", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "Bonus", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.Customers", "InCityId");
            AddForeignKey("dbo.Customers", "InCityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
