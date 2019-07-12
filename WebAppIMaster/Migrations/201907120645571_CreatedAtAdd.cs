namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedAtAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrganizationPromotionAndDiscounts", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrganizationPromotionAndDiscounts", "CreatedAt");
        }
    }
}
