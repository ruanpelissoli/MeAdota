namespace Adopcat.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertAppParameters : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [ApplicationParameter] VALUES('AzureStorageURL', 'AzureStorageURL', 'https://adopcatstorage.blob.core.windows.net')
                  INSERT INTO [ApplicationParameter] VALUES('AzureStoragePublicContainer', 'AzureStoragePublicContainer', 'public')
                  INSERT INTO [ApplicationParameter] VALUES('PetPicturesBlobName', 'PetPicturesBlobName', 'pets')
                  INSERT INTO [ApplicationParameter] VALUES('UserPicturesBlobName', 'UserPicturesBlobName', 'users')
                  INSERT INTO [ApplicationParameter] VALUES('AzureStorageConnectionString', 'AzureStorageConnectionString', 'DefaultEndpointsProtocol=https;AccountName=adopcatstorage;AccountKey=bIkBgP4UvdIe6g8vnb+JJ1mvKEUZOHqWPJsCJcDuXbTqEFipd5Nx31XsF4WZe+JyHMnAlM99ariWi6s0K0/twg==;EndpointSuffix=core.windows.net')");
        }
        
        public override void Down()
        {
        }
    }
}
