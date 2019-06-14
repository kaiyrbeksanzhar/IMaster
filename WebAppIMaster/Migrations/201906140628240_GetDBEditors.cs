namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GetDBEditors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookmarks",
                c => new
                    {
                        BookMarkUserId = c.String(nullable: false, maxLength: 128),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookMarkUserId, t.OrderId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Bookmarks");
        }
    }
}
