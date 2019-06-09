namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhoneChecking : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.PhoneCheckingCodes");
            AlterColumn("dbo.PhoneCheckingCodes", "CheckingCode", c => c.String());
            AddPrimaryKey("dbo.PhoneCheckingCodes", "PhoneNumber");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PhoneCheckingCodes");
            AlterColumn("dbo.PhoneCheckingCodes", "CheckingCode", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.PhoneCheckingCodes", new[] { "PhoneNumber", "CheckingCode" });
        }
    }
}
