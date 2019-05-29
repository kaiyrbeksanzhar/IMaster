namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UCityEdit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cities", "Latitude", c => c.String());
            AlterColumn("dbo.Cities", "Longitudey", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cities", "Longitudey", c => c.Int(nullable: false));
            AlterColumn("dbo.Cities", "Latitude", c => c.Int(nullable: false));
        }
    }
}
