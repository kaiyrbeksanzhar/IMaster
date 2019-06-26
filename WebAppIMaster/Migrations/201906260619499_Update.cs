namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Bookmarks", "BookMarkUserId");
            CreateIndex("dbo.Bookmarks", "OrderId");
            AddForeignKey("dbo.Bookmarks", "BookMarkUserId", "dbo.AspNetUsers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Bookmarks", "OrderId", "dbo.CustomerOrders", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookmarks", "OrderId", "dbo.CustomerOrders");
            DropForeignKey("dbo.Bookmarks", "BookMarkUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Bookmarks", new[] { "OrderId" });
            DropIndex("dbo.Bookmarks", new[] { "BookMarkUserId" });
        }
    }
}
