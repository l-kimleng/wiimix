namespace WiiMix.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateProductData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Apple iPad mini White & Silver 16 GB',4,1)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Dell Inspiron 15 3521',2,2)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Dell Inspiron 15R 5521',2,2)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Dell inspiron 2012',2,2)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Google Nexus 4 Black',6,4)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('HP 1000-1401AU Laptop',2,3)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Microsoft Windows 8 Pro 64-bit',7,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia 1100',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 1020',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 510',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 610',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 620',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 625',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 720',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 820',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 920',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 925',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Samsung Galaxy S4 Mini GT-I9192',6,9)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Samsung Galaxy Tab',9,9)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Sony Xperia C Black',6,11)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Sony Xperia L',6,11)");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Products WHERE Id IN(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21)");
        }
    }
}
