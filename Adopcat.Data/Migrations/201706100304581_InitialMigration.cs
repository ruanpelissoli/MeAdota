namespace Adopcat.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationParameter",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Key = c.String(nullable: false, maxLength: 64),
                        Value = c.String(nullable: false, maxLength: 512),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PetPicture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentType = c.String(),
                        Data = c.Binary(),
                        Name = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Poster",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PetType = c.Int(nullable: false),
                        Castrated = c.Boolean(nullable: false),
                        Dewormed = c.Boolean(nullable: false),
                        DeliverToAdopter = c.Boolean(nullable: false),
                        Country = c.String(),
                        State = c.String(),
                        City = c.String(),
                        IsAdopted = c.Boolean(nullable: false),
                        AdopterId = c.Int(nullable: false),
                        PetPicture_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.AdopterId)
                .ForeignKey("dbo.PetPicture", t => t.PetPicture_Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.AdopterId)
                .Index(t => t.PetPicture_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceId = c.String(),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(maxLength: 20),
                        PictureUrl = c.String(maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SystemLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        LogType = c.Int(nullable: false),
                        LogDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Access_token = c.String(nullable: false, maxLength: 512),
                        Token_type = c.String(nullable: false, maxLength: 56),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Token", "UserId", "dbo.User");
            DropForeignKey("dbo.Poster", "UserId", "dbo.User");
            DropForeignKey("dbo.Poster", "PetPicture_Id", "dbo.PetPicture");
            DropForeignKey("dbo.Poster", "AdopterId", "dbo.User");
            DropIndex("dbo.Token", new[] { "UserId" });
            DropIndex("dbo.Poster", new[] { "PetPicture_Id" });
            DropIndex("dbo.Poster", new[] { "AdopterId" });
            DropIndex("dbo.Poster", new[] { "UserId" });
            DropTable("dbo.Token");
            DropTable("dbo.SystemLog");
            DropTable("dbo.User");
            DropTable("dbo.Poster");
            DropTable("dbo.PetPicture");
            DropTable("dbo.ApplicationParameter");
        }
    }
}
