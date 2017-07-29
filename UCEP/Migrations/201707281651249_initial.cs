namespace UCEP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FsCatalogues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HospitalCode = c.String(nullable: false, maxLength: 20),
                        FSCodeNIEMS = c.String(nullable: false, maxLength: 50),
                        FSCodeHos = c.String(nullable: false, maxLength: 50),
                        Category = c.String(nullable: false, maxLength: 10),
                        Meaning = c.String(nullable: false),
                        Unit = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EffectiveDate = c.DateTime(nullable: false),
                        Status = c.String(nullable: false, maxLength: 50),
                        ApprovalDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FsCatalogues");
        }
    }
}
