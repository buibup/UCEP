namespace UCEP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FsCatalogues",
                c => new
                    {
                        FSCodeHos = c.String(nullable: false, maxLength: 50),
                        HospitalCode = c.String(nullable: false, maxLength: 20),
                        FSCodeNIEMS = c.String(nullable: false, maxLength: 50),
                        Category = c.String(nullable: false, maxLength: 10),
                        Meaning = c.String(),
                        Unit = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EffectiveDate = c.DateTime(),
                        Status = c.String(nullable: false, maxLength: 50),
                        ApprovalDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FSCodeHos);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FsCatalogues");
        }
    }
}
