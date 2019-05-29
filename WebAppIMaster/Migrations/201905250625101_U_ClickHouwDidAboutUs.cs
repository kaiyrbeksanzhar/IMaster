namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class U_ClickHouwDidAboutUs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HowDidYouAboutUs", "Click", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HowDidYouAboutUs", "Click");
        }
    }
}
