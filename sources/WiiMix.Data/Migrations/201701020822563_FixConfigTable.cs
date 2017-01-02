namespace WiiMix.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FixConfigTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockDetail", "ConfigId", "dbo.Configs");
            DropIndex("dbo.Configs", new[] { "Product_Id" });
            DropColumn("dbo.Configs", "ProductId");
            RenameColumn(table: "dbo.Configs", name: "Product_Id", newName: "ProductId");
            DropPrimaryKey("dbo.Configs");
            AlterColumn("dbo.Configs", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.Configs", "ProductId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Configs", "ProductId");
            CreateIndex("dbo.Configs", "ProductId");
            AddForeignKey("dbo.StockDetail", "ConfigId", "dbo.Configs", "ProductId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockDetail", "ConfigId", "dbo.Configs");
            DropIndex("dbo.Configs", new[] { "ProductId" });
            DropPrimaryKey("dbo.Configs");
            AlterColumn("dbo.Configs", "ProductId", c => c.Int());
            AlterColumn("dbo.Configs", "ProductId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Configs", "ProductId");
            RenameColumn(table: "dbo.Configs", name: "ProductId", newName: "Product_Id");
            AddColumn("dbo.Configs", "ProductId", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Configs", "Product_Id");
            AddForeignKey("dbo.StockDetail", "ConfigId", "dbo.Configs", "ProductId", cascadeDelete: false);
        }
    }
}
