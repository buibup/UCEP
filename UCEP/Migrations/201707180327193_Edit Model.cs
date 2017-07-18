namespace UCEP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FsCatalogues", "HospitalCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.FsCatalogues", "FSCodeNIEMS", c => c.String(maxLength: 50));
            AlterColumn("dbo.FsCatalogues", "FSCodeHos", c => c.String(maxLength: 50));
            AlterColumn("dbo.FsCatalogues", "Category", c => c.String(maxLength: 10));
            AlterColumn("dbo.FsCatalogues", "Unit", c => c.String(maxLength: 50));
            AlterColumn("dbo.FsCatalogues", "Status", c => c.String(maxLength: 50));
            AlterColumn("dbo.FsTemplates", "FSCodeOrTMTCode", c => c.String(maxLength: 50));
            AlterColumn("dbo.FsTemplates", "HospitalCode", c => c.String(maxLength: 50));
            AlterColumn("dbo.FsTemplates", "Category", c => c.String(maxLength: 10));
            AlterColumn("dbo.FsTemplates", "Unit", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FsTemplates", "Unit", c => c.String());
            AlterColumn("dbo.FsTemplates", "Category", c => c.String());
            AlterColumn("dbo.FsTemplates", "HospitalCode", c => c.String());
            AlterColumn("dbo.FsTemplates", "FSCodeOrTMTCode", c => c.String());
            AlterColumn("dbo.FsCatalogues", "Status", c => c.String());
            AlterColumn("dbo.FsCatalogues", "Unit", c => c.String());
            AlterColumn("dbo.FsCatalogues", "Category", c => c.String());
            AlterColumn("dbo.FsCatalogues", "FSCodeHos", c => c.String());
            AlterColumn("dbo.FsCatalogues", "FSCodeNIEMS", c => c.String());
            AlterColumn("dbo.FsCatalogues", "HospitalCode", c => c.String());
        }
    }
}
