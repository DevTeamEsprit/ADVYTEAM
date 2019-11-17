namespace ADVYTEAM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recl : DbMigration
    {
        public override void Up()
        {
            
            
            CreateTable(
                "dbo.reclamations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Objet = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        DateReclamation = c.DateTime(nullable: false, precision: 0),
                        DateTraitement = c.DateTime(nullable: false, precision: 0),
                        UserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.utilisateur", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
           
            
        }
        
        public override void Down()
        {
            
 
        }
    }
}
