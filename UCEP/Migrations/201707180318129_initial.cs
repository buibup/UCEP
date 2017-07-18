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
                        HospitalCode = c.String(),
                        FSCodeNIEMS = c.String(),
                        FSCodeHos = c.String(),
                        Category = c.String(),
                        Meaning = c.String(),
                        Unit = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EffectiveDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        ApprovalDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FsTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UseDate = c.DateTime(nullable: false),
                        FSCodeOrTMTCode = c.String(),
                        HospitalCode = c.String(),
                        Category = c.String(),
                        Mean = c.String(),
                        Unit = c.String(),
                        PriceTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FsTemplates");
            DropTable("dbo.FsCatalogues");
        }
    }
}
