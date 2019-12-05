namespace ADVYTEAM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.reclamations", "Etat", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.reclamations", "Etat", c => c.Boolean(nullable: false));
        }
    }
}
