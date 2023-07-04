namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DesignationName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employee2",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DOB = c.DateTime(nullable: false),
                        Mobile_Number = c.String(nullable: false, maxLength: 10),
                        Address = c.String(maxLength: 100),
                        Salary = c.Int(nullable: false),
                        DesignationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Designations", t => t.DesignationID, cascadeDelete: true)
                .Index(t => t.DesignationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee2", "DesignationID", "dbo.Designations");
            DropIndex("dbo.Employee2", new[] { "DesignationID" });
            DropTable("dbo.Employee2");
            DropTable("dbo.Designations");
        }
    }
}
