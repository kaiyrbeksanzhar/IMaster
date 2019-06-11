namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbEditor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "HowDidYouAboutUsId", c => c.Int());
            AlterColumn("dbo.HowDidYouAboutUs", "Order", c => c.Int());
            CreateIndex("dbo.Customers", "HowDidYouAboutUsId");
            AddForeignKey("dbo.Customers", "HowDidYouAboutUsId", "dbo.HowDidYouAboutUs", "Id");
            DropColumn("dbo.HowDidYouAboutUs", "Click");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HowDidYouAboutUs", "Click", c => c.Int());
            DropForeignKey("dbo.Customers", "HowDidYouAboutUsId", "dbo.HowDidYouAboutUs");
            DropIndex("dbo.Customers", new[] { "HowDidYouAboutUsId" });
            AlterColumn("dbo.HowDidYouAboutUs", "Order", c => c.Int(nullable: false));
            DropColumn("dbo.Customers", "HowDidYouAboutUsId");
        }
    }
}
