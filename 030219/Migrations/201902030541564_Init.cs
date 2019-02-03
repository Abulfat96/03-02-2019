namespace _030219.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrendId = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brends", t => t.BrendId, cascadeDelete: true)
                .Index(t => t.BrendId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Models", "BrendId", "dbo.Brends");
            DropIndex("dbo.Models", new[] { "BrendId" });
            DropTable("dbo.Models");
            DropTable("dbo.Brends");
        }
    }
}
