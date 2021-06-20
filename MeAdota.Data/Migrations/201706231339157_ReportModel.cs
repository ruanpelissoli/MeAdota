namespace MeAdota.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PosterId = c.Int(nullable: false),
                        Motive = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Poster", t => t.PosterId)
                .Index(t => t.PosterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "PosterId", "dbo.Poster");
            DropIndex("dbo.Reports", new[] { "PosterId" });
            DropTable("dbo.Reports");
        }
    }
}
