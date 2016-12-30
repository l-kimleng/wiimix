namespace WiiMix.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateDataToDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories(Name) VALUES('Card Reader')");
            Sql("INSERT INTO Categories(Name) VALUES('Computers')");
            Sql("INSERT INTO Categories(Name) VALUES('Headphones')");
            Sql("INSERT INTO Categories(Name) VALUES('IPad')");
            Sql("INSERT INTO Categories(Name) VALUES('Memory Cards')");
            Sql("INSERT INTO Categories(Name) VALUES('Mobiles')");
            Sql("INSERT INTO Categories(Name) VALUES('Operating System')");
            Sql("INSERT INTO Categories(Name) VALUES('Pen Drives')");
            Sql("INSERT INTO Categories(Name) VALUES('Tablets')");

            Sql("INSERT INTO Brands(Name) VALUES('Apple')");
            Sql("INSERT INTO Brands(Name) VALUES('Dell')");
            Sql("INSERT INTO Brands(Name) VALUES('HP')");
            Sql("INSERT INTO Brands(Name) VALUES('LG')");
            Sql("INSERT INTO Brands(Name) VALUES('Microsoft Corporations')");
            Sql("INSERT INTO Brands(Name) VALUES('Moserbear')");
            Sql("INSERT INTO Brands(Name) VALUES('Panasonic')");
            Sql("INSERT INTO Brands(Name) VALUES('Philips')");
            Sql("INSERT INTO Brands(Name) VALUES('Samsung Electronics ltd.')");
            Sql("INSERT INTO Brands(Name) VALUES('Sandisk')");
            Sql("INSERT INTO Brands(Name) VALUES('Sony Inc.')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Categories WHERE Id IN (1, 2, 3, 4, 5, 6, 7, 8, 9)");
            Sql("DELETE FROM Brands WHERE Id IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11)");
        }
    }
}
