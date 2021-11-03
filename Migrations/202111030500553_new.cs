namespace AppDevelopment0805.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "UserId" });
            DropColumn("dbo.Courses", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Courses", "UserId");
            AddForeignKey("dbo.Courses", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
