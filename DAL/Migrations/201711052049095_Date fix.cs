namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Datefix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "RegisteredDate", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "StudyDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "StudyDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "RegisteredDate", c => c.DateTime(nullable: false));
        }
    }
}
