namespace WiiMix.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitalizeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 125, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        CategoryId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Configs",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Feature = c.String(maxLength: 300),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        Image = c.String(maxLength: 125, unicode: false),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Quantity = c.Single(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockDetail",
                c => new
                    {
                        StockId = c.Int(nullable: false),
                        ConfigId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StockId, t.ConfigId })
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .ForeignKey("dbo.Configs", t => t.ConfigId, cascadeDelete: true)
                .Index(t => t.StockId)
                .Index(t => t.ConfigId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Configs", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.StockDetail", "ConfigId", "dbo.Configs");
            DropForeignKey("dbo.StockDetail", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropIndex("dbo.StockDetail", new[] { "ConfigId" });
            DropIndex("dbo.StockDetail", new[] { "StockId" });
            DropIndex("dbo.Configs", new[] { "Product_Id" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.StockDetail");
            DropTable("dbo.Stocks");
            DropTable("dbo.Configs");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Brands");
        }
    }
}
