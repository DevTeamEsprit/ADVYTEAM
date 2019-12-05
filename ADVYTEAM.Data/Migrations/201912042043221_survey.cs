namespace ADVYTEAM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class survey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.surveyquestemploye",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        employeId = c.Long(nullable: false),
                        surveyQuestId = c.Int(nullable: false),
                        comment = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.utilisateur", t => t.employeId, cascadeDelete: true)
                .ForeignKey("dbo.surveyquestion", t => t.surveyQuestId, cascadeDelete: true)
                .Index(t => t.employeId)
                .Index(t => t.surveyQuestId);
            
            CreateTable(
                "dbo.surveyquestion",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        question = c.String(unicode: false),
                        managerId = c.Long(nullable: false),
                        employeId = c.Long(nullable: false),
                        date = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.survey", t => new { t.managerId, t.employeId, t.date })
                .Index(t => new { t.managerId, t.employeId, t.date });
            
            CreateTable(
                "dbo.survey",
                c => new
                    {
                        managerId = c.Long(nullable: false),
                        employeId = c.Long(nullable: false),
                        date = c.DateTime(nullable: false, precision: 0),
                        duree = c.Int(nullable: false),
                        status = c.Boolean(nullable: false, storeType: "bit"),
                    })
                .PrimaryKey(t => new { t.managerId, t.employeId, t.date })
                .ForeignKey("dbo.utilisateur", t => t.employeId, cascadeDelete: true)
                .ForeignKey("dbo.utilisateur", t => t.managerId, cascadeDelete: true)
                .Index(t => t.managerId)
                .Index(t => t.employeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.surveyquestemploye", "surveyQuestId", "dbo.surveyquestion");
            DropForeignKey("dbo.surveyquestion", new[] { "managerId", "employeId", "date" }, "dbo.survey");
            DropForeignKey("dbo.survey", "managerId", "dbo.utilisateur");
            DropForeignKey("dbo.survey", "employeId", "dbo.utilisateur");
            DropForeignKey("dbo.surveyquestemploye", "employeId", "dbo.utilisateur");
            DropIndex("dbo.survey", new[] { "employeId" });
            DropIndex("dbo.survey", new[] { "managerId" });
            DropIndex("dbo.surveyquestion", new[] { "managerId", "employeId", "date" });
            DropIndex("dbo.surveyquestemploye", new[] { "surveyQuestId" });
            DropIndex("dbo.surveyquestemploye", new[] { "employeId" });
            DropTable("dbo.survey");
            DropTable("dbo.surveyquestion");
            DropTable("dbo.surveyquestemploye");
        }
    }
}
