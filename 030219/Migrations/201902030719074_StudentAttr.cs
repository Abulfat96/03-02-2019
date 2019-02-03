namespace _030219.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentAttr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Surname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Surname", c => c.String());
            AlterColumn("dbo.Students", "Name", c => c.String());
        }
    }
}
