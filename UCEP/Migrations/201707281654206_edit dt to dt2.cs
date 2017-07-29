namespace UCEP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editdttodt2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FsCatalogues", "EffectiveDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FsCatalogues", "EffectiveDate", c => c.DateTime(nullable: false));
        }
    }
}
