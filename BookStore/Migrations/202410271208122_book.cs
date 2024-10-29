namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class book : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Author = c.String(maxLength: 255),
                        PublishingHouseName = c.String(maxLength: 255),
                        PageCount = c.Int(nullable: false),
                        Genre = c.String(maxLength: 100),
                        Sequel = c.String(maxLength: 100),
                        PrimeCost = c.Single(nullable: false),
                        SalePrice = c.Single(nullable: false),
                        DatePublished = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Books");
        }
    }
}
