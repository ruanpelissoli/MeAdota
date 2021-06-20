namespace MeAdota.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRegistrationIdAndNotificationOption : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "RegistrationId", c => c.String());
            AddColumn("dbo.User", "ReceiveNotifications", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "ReceiveNotifications");
            DropColumn("dbo.User", "RegistrationId");
        }
    }
}
