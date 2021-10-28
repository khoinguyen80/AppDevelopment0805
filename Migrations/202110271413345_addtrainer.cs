namespace AppDevelopment0805.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtrainer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrainersCourses",
                c => new
                    {
                        TrainerId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrainerId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Trainers", t => t.TrainerId, cascadeDelete: true)
                .Index(t => t.TrainerId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainersCourses", "TrainerId", "dbo.Trainers");
            DropForeignKey("dbo.TrainersCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.TrainersCourses", new[] { "CourseId" });
            DropIndex("dbo.TrainersCourses", new[] { "TrainerId" });
            DropTable("dbo.TrainersCourses");
        }
    }
}
