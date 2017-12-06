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
                        ID = c.Long(nullable: false, identity: true),
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
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TestProduct",
                c => new
                    {
                        Test_ID = c.Long(nullable: false),
                        Product_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Test_ID, t.Product_ID })
                .ForeignKey("dbo.Test", t => t.Test_ID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Test_ID)
                .Index(t => t.Product_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestProduct", "Product_ID", "dbo.Product");
            DropForeignKey("dbo.TestProduct", "Test_ID", "dbo.Test");
            DropIndex("dbo.TestProduct", new[] { "Product_ID" });
            DropIndex("dbo.TestProduct", new[] { "Test_ID" });
            DropTable("dbo.TestProduct");
            DropTable("dbo.Test");
            DropTable("dbo.Product");
        }
    }
}
