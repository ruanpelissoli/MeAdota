namespace MeAdota.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertAppParameters : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [ApplicationParameter] VALUES('AzureStorageURL', 'AzureStorageURL', 'https://meadota.blob.core.windows.net/')
                  INSERT INTO [ApplicationParameter] VALUES('AzureStoragePublicContainer', 'AzureStoragePublicContainer', 'public')
                  INSERT INTO [ApplicationParameter] VALUES('PetPicturesBlobName', 'PetPicturesBlobName', 'pets')
                  INSERT INTO [ApplicationParameter] VALUES('UserPicturesBlobName', 'UserPicturesBlobName', 'users')
                  INSERT INTO [ApplicationParameter] VALUES('AzureStorageConnectionString', 'AzureStorageConnectionString', 'DefaultEndpointsProtocol=https;AccountName=meadota;AccountKey=LBvQuTtqRvgOUYlmxrgWEf3alwSzCBzod7WbBh3rlPb45eGxHHuU0Dp6nuO0dt/rdphNQ+Ck3sULKogchZizpg==;EndpointSuffix=core.windows.net')");
        }
        
        public override void Down()
        {
        }
    }
}
