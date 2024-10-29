namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class promotion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        PromotionId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        CustomerId = c.Int(),
                        PromotionTitle = c.String(maxLength: 255),
                        DiscountPercentage = c.Single(nullable: false),
                        PriceAfterDiscount = c.Single(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Ended = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PromotionId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.BookId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Promotions", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Promotions", "BookId", "dbo.Books");
            DropIndex("dbo.Promotions", new[] { "CustomerId" });
            DropIndex("dbo.Promotions", new[] { "BookId" });
            DropTable("dbo.Promotions");
        }
    }
}
