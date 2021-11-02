namespace AppDevelopment0805.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraineesCourses",
                c => new
                    {
                        TraineeId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TraineeId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Trainees", t => t.TraineeId, cascadeDelete: true)
                .Index(t => t.TraineeId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraineesCourses", "TraineeId", "dbo.Trainees");
            DropForeignKey("dbo.TraineesCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.TraineesCourses", new[] { "CourseId" });
            DropIndex("dbo.TraineesCourses", new[] { "TraineeId" });
            DropTable("dbo.TraineesCourses");
        }
    }
}
