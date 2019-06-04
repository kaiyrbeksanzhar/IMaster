namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbCommit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExecutorServices", "CostType", c => c.Int(nullable: false));
            AddColumn("dbo.ExecutorServices", "Name", c => c.String());
            AddColumn("dbo.ExecutorServices", "FromCost", c => c.Int(nullable: false));
            AddColumn("dbo.ExecutorServices", "ToCost", c => c.Int(nullable: false));
            AddColumn("dbo.ExecutorServices", "FixedCost", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "GenderName");
            DropColumn("dbo.ExecutorServices", "Cost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExecutorServices", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.AspNetUsers", "GenderName", c => c.String());
            DropColumn("dbo.ExecutorServices", "FixedCost");
            DropColumn("dbo.ExecutorServices", "ToCost");
            DropColumn("dbo.ExecutorServices", "FromCost");
            DropColumn("dbo.ExecutorServices", "Name");
            DropColumn("dbo.ExecutorServices", "CostType");
        }
    }
}
