namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpecilizationId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerOrders", "CategoryId", "dbo.Categories");
            DropIndex("dbo.CustomerOrders", new[] { "CategoryId" });
            AddColumn("dbo.CustomerOrders", "SpecializationId", c => c.Int(nullable: true));
            CreateIndex("dbo.CustomerOrders", "SpecializationId");
            AddForeignKey("dbo.CustomerOrders", "SpecializationId", "dbo.Specializations", "Id", cascadeDelete: false);
            DropColumn("dbo.CustomerOrders", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerOrders", "CategoryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CustomerOrders", "SpecializationId", "dbo.Specializations");
            DropIndex("dbo.CustomerOrders", new[] { "SpecializationId" });
            DropColumn("dbo.CustomerOrders", "SpecializationId");
            CreateIndex("dbo.CustomerOrders", "CategoryId");
            AddForeignKey("dbo.CustomerOrders", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
