namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPriority : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Priority", c => c.Int(nullable: false));
            DropColumn("dbo.CategoryLangs", "Priority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CategoryLangs", "Priority", c => c.Int(nullable: false));
            DropColumn("dbo.Categories", "Priority");
        }
    }
}
