namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhoneNumberChecking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhoneCheckingCodes",
                c => new
                    {
                        PhoneNumber = c.String(nullable: false, maxLength: 128),
                        CheckingCode = c.String(nullable: false, maxLength: 128),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.PhoneNumber, t.CheckingCode });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PhoneCheckingCodes");
        }
    }
}
