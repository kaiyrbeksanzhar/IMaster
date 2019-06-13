namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbEditcurrent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerOrders", "OrderType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerOrders", "OrderType");
        }
    }
}
