namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbDatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Executors", "BirthDay", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Executors", "BirthDay", c => c.DateTime(nullable: false));
        }
    }
}
