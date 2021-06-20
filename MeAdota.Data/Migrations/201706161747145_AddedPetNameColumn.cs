namespace MeAdota.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPetNameColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Poster", "PetName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Poster", "PetName");
        }
    }
}
