namespace NPV.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cashflows",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NPVCalculationsID = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.NPVCalculations", t => t.NPVCalculationsID, cascadeDelete: true)
                .Index(t => t.NPVCalculationsID);
            
            CreateTable(
                "dbo.NPVCalculations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CalculationDate = c.DateTime(nullable: false),
                        InitialValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LowerBoundDiscountRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UpperBoundDiscountRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountRateIncrement = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NPVCalculations1",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NPVCalculationsID = c.Int(nullable: false),
                        NPV = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.NPVCalculations", t => t.NPVCalculationsID, cascadeDelete: true)
                .Index(t => t.NPVCalculationsID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cashflows", "NPVCalculationsID", "dbo.NPVCalculations");
            DropForeignKey("dbo.NPVCalculations1", "NPVCalculationsID", "dbo.NPVCalculations");
            DropIndex("dbo.NPVCalculations1", new[] { "NPVCalculationsID" });
            DropIndex("dbo.Cashflows", new[] { "NPVCalculationsID" });
            DropTable("dbo.NPVCalculations1");
            DropTable("dbo.NPVCalculations");
            DropTable("dbo.Cashflows");
        }
    }
}
