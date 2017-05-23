namespace Zoo.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        BirthDate = c.DateTimeOffset(nullable: false, precision: 7),
                        SpecieId = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedAt = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Species", t => t.SpecieId)
                .Index(t => t.SpecieId);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedAt = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Animals", "SpecieId", "dbo.Species");
            DropIndex("dbo.Animals", new[] { "SpecieId" });
            DropTable("dbo.Species");
            DropTable("dbo.Animals");
        }
    }
}
