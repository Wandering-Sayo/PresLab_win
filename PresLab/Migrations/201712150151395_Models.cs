namespace PresLab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientID = c.String(),
                        RequestDate = c.DateTime(nullable: false),
                        Client_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Client", t => t.Client_ID)
                .Index(t => t.Client_ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Brand = c.String(),
                        Description = c.String(),
                        Supplier = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        TestID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.TestID);
            
            CreateTable(
                "dbo.Samplings",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.OrderId })
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.TestProduct",
                c => new
                    {
                        Test_TestID = c.Int(nullable: false),
                        Product_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Test_TestID, t.Product_ID })
                .ForeignKey("dbo.Test", t => t.Test_TestID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Test_TestID)
                .Index(t => t.Product_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestProduct", "Product_ID", "dbo.Product");
            DropForeignKey("dbo.TestProduct", "Test_TestID", "dbo.Test");
            DropForeignKey("dbo.Samplings", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Samplings", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Order", "Client_ID", "dbo.Client");
            DropIndex("dbo.TestProduct", new[] { "Product_ID" });
            DropIndex("dbo.TestProduct", new[] { "Test_TestID" });
            DropIndex("dbo.Samplings", new[] { "OrderId" });
            DropIndex("dbo.Samplings", new[] { "ProductId" });
            DropIndex("dbo.Order", new[] { "Client_ID" });
            DropTable("dbo.TestProduct");
            DropTable("dbo.Samplings");
            DropTable("dbo.Test");
            DropTable("dbo.Product");
            DropTable("dbo.Order");
            DropTable("dbo.Client");
        }
    }
}
