namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withoutannotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String());
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
