namespace PresLab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Sampling",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ClientID = c.String(),
                        RequestDate = c.DateTime(nullable: false),
                        Client_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Client", t => t.Client_ID)
                .Index(t => t.Client_ID);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        TestID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestID);
            
            CreateTable(
                "dbo.SamplingProduct",
                c => new
                    {
                        Sampling_ID = c.Long(nullable: false),
                        Product_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sampling_ID, t.Product_ID })
                .ForeignKey("dbo.Sampling", t => t.Sampling_ID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Sampling_ID)
                .Index(t => t.Product_ID);
            
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
            DropForeignKey("dbo.SamplingProduct", "Product_ID", "dbo.Product");
            DropForeignKey("dbo.SamplingProduct", "Sampling_ID", "dbo.Sampling");
            DropForeignKey("dbo.Sampling", "Client_ID", "dbo.Client");
            DropIndex("dbo.TestProduct", new[] { "Product_ID" });
            DropIndex("dbo.TestProduct", new[] { "Test_TestID" });
            DropIndex("dbo.SamplingProduct", new[] { "Product_ID" });
            DropIndex("dbo.SamplingProduct", new[] { "Sampling_ID" });
            DropIndex("dbo.Sampling", new[] { "Client_ID" });
            DropTable("dbo.TestProduct");
            DropTable("dbo.SamplingProduct");
            DropTable("dbo.Test");
            DropTable("dbo.Client");
            DropTable("dbo.Sampling");
            DropTable("dbo.Product");
        }
    }
}
