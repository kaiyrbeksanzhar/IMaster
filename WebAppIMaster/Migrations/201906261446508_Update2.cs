namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CancelExecutorResponses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CancelExecutorResponses",
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
