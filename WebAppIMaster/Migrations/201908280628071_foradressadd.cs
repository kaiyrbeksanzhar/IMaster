namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foradressadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "Plus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "Plus");
        }
    }
}
