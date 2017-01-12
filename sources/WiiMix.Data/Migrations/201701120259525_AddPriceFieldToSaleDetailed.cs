namespace WiiMix.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceFieldToSaleDetailed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockDetails", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockDetails", "Price");
        }
    }
}
