namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Support : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Supports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        TypeMessage = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        CityId = c.Int(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supports", "CityId", "dbo.Cities");
            DropIndex("dbo.Supports", new[] { "CityId" });
            DropTable("dbo.Supports");
        }
    }
}
