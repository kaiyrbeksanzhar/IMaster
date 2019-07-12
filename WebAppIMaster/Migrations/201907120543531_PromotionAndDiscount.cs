namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PromotionAndDiscount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrganizationPromotionAndDiscounts", "DateTimeCanceled", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrganizationPromotionAndDiscounts", "DateTimeCanceled");
        }
    }
}
