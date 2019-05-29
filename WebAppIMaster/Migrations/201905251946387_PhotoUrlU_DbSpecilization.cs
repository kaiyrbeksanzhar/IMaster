namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoUrlU_DbSpecilization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Specializations", "PhotoUrl", c => c.String());
            DropColumn("dbo.SpecializationLangs", "PhotoUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SpecializationLangs", "PhotoUrl", c => c.String());
            DropColumn("dbo.Specializations", "PhotoUrl");
        }
    }
}
