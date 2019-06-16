namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBEDTIR : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Gender");
        }
    }
}
