namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerFileType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Photo", c => c.Binary());
            AddColumn("dbo.Customers", "AvatarType", c => c.String());
            DropColumn("dbo.Customers", "AvatarUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "AvatarUrl", c => c.String());
            DropColumn("dbo.Customers", "AvatarType");
            DropColumn("dbo.Customers", "Photo");
        }
    }
}
