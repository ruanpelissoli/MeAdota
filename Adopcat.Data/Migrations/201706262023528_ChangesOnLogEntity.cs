namespace Adopcat.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesOnLogEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SystemLog", "Platform", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SystemLog", "Platform");
        }
    }
}
