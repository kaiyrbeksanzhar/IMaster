namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Commit12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientPhones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(),
                        MarketId = c.Int(nullable: false),
                        CreatedAt_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExecutorPhones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExecutorId = c.String(),
                        MarketId = c.Int(nullable: false),
                        CreatedAt_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExecutorPhones");
            DropTable("dbo.ClientPhones");
        }
    }
}
