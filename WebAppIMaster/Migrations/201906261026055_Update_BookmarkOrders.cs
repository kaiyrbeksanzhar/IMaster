namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_BookmarkOrders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookmarks", "BookMarkUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookmarks", "OrderId", "dbo.CustomerOrders");
            DropIndex("dbo.Bookmarks", new[] { "BookMarkUserId" });
            DropIndex("dbo.Bookmarks", new[] { "OrderId" });
            CreateTable(
                "dbo.BookmarkOrders",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.OrderId })
                .ForeignKey("dbo.CustomerOrders", t => t.OrderId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.OrderId);
            
            DropTable("dbo.Bookmarks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Bookmarks",
                c => new
                    {
                        BookMarkUserId = c.String(nullable: false, maxLength: 128),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookMarkUserId, t.OrderId });
            
            DropForeignKey("dbo.BookmarkOrders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookmarkOrders", "OrderId", "dbo.CustomerOrders");
            DropIndex("dbo.BookmarkOrders", new[] { "OrderId" });
            DropIndex("dbo.BookmarkOrders", new[] { "UserId" });
            DropTable("dbo.BookmarkOrders");
            CreateIndex("dbo.Bookmarks", "OrderId");
            CreateIndex("dbo.Bookmarks", "BookMarkUserId");
            AddForeignKey("dbo.Bookmarks", "OrderId", "dbo.CustomerOrders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bookmarks", "BookMarkUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
