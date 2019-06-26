namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.AddExecutorToOrders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AddExecutorToOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(),
                        OrderId = c.Int(nullable: false),
                        ExecutorId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
