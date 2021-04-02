namespace Library.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LibraryFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookBoughts", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookBoughts", "BookID", "dbo.Books");
            DropIndex("dbo.BookBoughts", new[] { "BookID" });
            DropIndex("dbo.BookBoughts", new[] { "UserID" });
            CreateTable(
                "dbo.Shelves",
                c => new
                    {
                        ShelfID = c.Int(nullable: false, identity: true),
                        BookID = c.Int(),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ShelfID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.Books", t => t.BookID)
                .Index(t => t.BookID)
                .Index(t => t.UserID);
            
            DropTable("dbo.BookBoughts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookBoughts",
                c => new
                    {
                        BookBoughtID = c.Int(nullable: false, identity: true),
                        BookID = c.Int(),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BookBoughtID);
            
            DropForeignKey("dbo.Shelves", "BookID", "dbo.Books");
            DropForeignKey("dbo.Shelves", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Shelves", new[] { "UserID" });
            DropIndex("dbo.Shelves", new[] { "BookID" });
            DropTable("dbo.Shelves");
            CreateIndex("dbo.BookBoughts", "UserID");
            CreateIndex("dbo.BookBoughts", "BookID");
            AddForeignKey("dbo.BookBoughts", "BookID", "dbo.Books", "BookID");
            AddForeignKey("dbo.BookBoughts", "UserID", "dbo.AspNetUsers", "Id");
        }
    }
}
