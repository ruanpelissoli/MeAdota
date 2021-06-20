namespace MeAdota.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDeviceIdToFacebookId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "FacebookId", c => c.String());
            DropColumn("dbo.User", "DeviceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "DeviceId", c => c.String());
            DropColumn("dbo.User", "FacebookId");
        }
    }
}
