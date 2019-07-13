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
                        CalculationID = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Calculations", t => t.CalculationID, cascadeDelete: true)
                .Index(t => t.CalculationID);
            
            CreateTable(
                "dbo.Calculations",
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
                "dbo.SingleNPVCalculations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CalculationID = c.Int(nullable: false),
                        DiscountRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NPV = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Calculations", t => t.CalculationID, cascadeDelete: true)
                .Index(t => t.CalculationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cashflows", "CalculationID", "dbo.Calculations");
            DropForeignKey("dbo.SingleNPVCalculations", "CalculationID", "dbo.Calculations");
            DropIndex("dbo.SingleNPVCalculations", new[] { "CalculationID" });
            DropIndex("dbo.Cashflows", new[] { "CalculationID" });
            DropTable("dbo.SingleNPVCalculations");
            DropTable("dbo.Calculations");
            DropTable("dbo.Cashflows");
        }
    }
}
