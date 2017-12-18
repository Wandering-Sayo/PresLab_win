namespace PresLab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Client", t => t.ClientID, cascadeDelete: true)
                .Index(t => t.ClientID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Brand = c.String(),
                        Description = c.String(),
                        Supplier = c.String(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
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
            DropForeignKey("dbo.Product", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.Order", "ClientID", "dbo.Client");
            DropIndex("dbo.TestProduct", new[] { "Product_ID" });
            DropIndex("dbo.TestProduct", new[] { "Test_TestID" });
            DropIndex("dbo.Samplings", new[] { "OrderId" });
            DropIndex("dbo.Samplings", new[] { "ProductId" });
            DropIndex("dbo.Product", new[] { "CategoryID" });
            DropIndex("dbo.Order", new[] { "ClientID" });
            DropTable("dbo.TestProduct");
            DropTable("dbo.Samplings");
            DropTable("dbo.Test");
            DropTable("dbo.Product");
            DropTable("dbo.Order");
            DropTable("dbo.Client");
            DropTable("dbo.Category");
        }
    }
}
