namespace ReferenceOfPerson.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CallSite = c.String(),
                        Date = c.String(),
                        Level = c.String(),
                        Logger = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Gender = c.Byte(),
                        PersonalNumber = c.String(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhoneNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false, maxLength: 50),
                        PersonId = c.Int(nullable: false),
                        Type = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persons", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Relationships",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        RelatedPersonId = c.Int(nullable: false),
                        Type = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonId, t.RelatedPersonId, t.Type })
                .ForeignKey("dbo.Persons", t => t.RelatedPersonId)
                .ForeignKey("dbo.Persons", t => t.PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.RelatedPersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Relationships", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.Relationships", "RelatedPersonId", "dbo.Persons");
            DropForeignKey("dbo.PhoneNumbers", "PersonId", "dbo.Persons");
            DropIndex("dbo.Relationships", new[] { "RelatedPersonId" });
            DropIndex("dbo.Relationships", new[] { "PersonId" });
            DropIndex("dbo.PhoneNumbers", new[] { "PersonId" });
            DropTable("dbo.Relationships");
            DropTable("dbo.PhoneNumbers");
            DropTable("dbo.Persons");
            DropTable("dbo.Logs");
        }
    }
}
