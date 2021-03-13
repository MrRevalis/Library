namespace Library.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Library_v1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ImageData", c => c.Binary());
            AddColumn("dbo.Books", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ImageMimeType");
            DropColumn("dbo.Books", "ImageData");
        }
    }
}
