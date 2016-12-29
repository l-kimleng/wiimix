namespace WiiMix.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateDataToDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories(Id, Name) VALUES(1, 'Card Reader')");
            Sql("INSERT INTO Categories(Id, Name) VALUES(2, 'Computers')");
            Sql("INSERT INTO Categories(Id, Name) VALUES(3, 'Headphones')");
            Sql("INSERT INTO Categories(Id, Name) VALUES(4, 'Ipad')");
            Sql("INSERT INTO Categories(Id, Name) VALUES(5, 'Memory Cards')");
            Sql("INSERT INTO Categories(Id, Name) VALUES(6, 'Mobiles')");
            Sql("INSERT INTO Categories(Id, Name) VALUES(7, 'Operating System')");
            Sql("INSERT INTO Categories(Id, Name) VALUES(8, 'Pen Drives')");
            Sql("INSERT INTO Categories(Id, Name) VALUES(9, 'Tablets')");

            Sql("INSERT INTO Brands(Id, Name) VALUES(1, 'Apple')");
            Sql("INSERT INTO Brands(Id, Name) VALUES(2, 'Dell')");
            Sql("INSERT INTO Brands(Id, Name) VALUES(3, 'HP')");
            Sql("INSERT INTO Brands(Id, Name) VALUES(4, 'LG')");
            Sql("INSERT INTO Brands(Id, Name) VALUES(5, 'Microsoft Corporations')");
            Sql("INSERT INTO Brands(Id, Name) VALUES(6, 'Moserbear')");
            Sql("INSERT INTO Brands(Id, Name) VALUES(7, 'Panasonic')");
            Sql("INSERT INTO Brands(Id, Name) VALUES(8, 'Philips')");
            Sql("INSERT INTO Brands(Id, Name) VALUES(9, 'Samsung Electronics ltd.')");
            Sql("INSERT INTO Brands(Id, Name) VALUES(10, 'Sandisk')");
            Sql("INSERT INTO Brands(Id, Name) VALUES(11, 'Sony Inc.')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Categories WHERE Id IN (1, 2, 3, 4, 5, 6, 7, 8, 9)");
            Sql("DELETE FROM Brands WHERE Id IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11)");
        }
    }
}
