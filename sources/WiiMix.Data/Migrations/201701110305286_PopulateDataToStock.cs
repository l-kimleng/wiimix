namespace WiiMix.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateDataToStock : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Stocks(Date, Quantity, TotalPrice) VALUES('2017-01-02',2,5000)");
            Sql("INSERT INTO Stocks(Date, Quantity, TotalPrice) VALUES('2017-01-02',5,15000)");
            Sql("INSERT INTO Stocks(Date, Quantity, TotalPrice) VALUES('2017-01-03',1,2500)");
            Sql("INSERT INTO Stocks(Date, Quantity, TotalPrice) VALUES('2017-01-04',3,7500)");
            Sql("INSERT INTO Stocks(Date, Quantity, TotalPrice) VALUES('2017-01-06',2,2500)");
            Sql("INSERT INTO Stocks(Date, Quantity, TotalPrice) VALUES('2017-01-07',4,10000)");
            Sql("INSERT INTO Stocks(Date, Quantity, TotalPrice) VALUES('2017-01-10',5,15000)");
            Sql("INSERT INTO Stocks(Date, Quantity, TotalPrice) VALUES('2017-01-10',1,2500)");
            Sql("INSERT INTO Stocks(Date, Quantity, TotalPrice) VALUES('2017-01-11',2,5000)");
            Sql("INSERT INTO Stocks(Date, Quantity, TotalPrice) VALUES('2017-01-11',6,17500)");
            Sql("INSERT INTO Stocks(Date, Quantity, TotalPrice) VALUES('2017-01-11',6,17500)");

            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(1,1,2)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(1,2,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(2,2,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(3,2,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(4,2,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(5,2,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(21,3,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(12,4,2)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(13,4,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(10,5,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(20,5,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(1,6,2)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(2,6,2)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(17,7,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(18,7,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(21,7,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(9,7,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(8,7,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(4,8,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(2,9,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(21,9,1)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(20,10,3)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(21,10,3)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(2,11,2)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(17,11,2)");
            Sql("INSERT INTO StockDetails(ProductId, StockId, Quantity) VALUES(20,11,2)");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM StockDetails WHERE Id IN(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21)");
            Sql("DELETE FROM Stocks WHERE Id IN(1,2,3,4,5,6,7,8,9,10,11)");
        }
    }
}
