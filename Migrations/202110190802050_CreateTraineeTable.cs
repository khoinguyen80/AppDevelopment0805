namespace AppDevelopment0805.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTraineeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Age = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                        StaffId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.StaffId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.Trainees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        DateOfBirth = c.DateTime(nullable: false),
                        Age = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                        Education = c.String(nullable: false),
                        TraineeId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TraineeId)
                .Index(t => t.TraineeId);
            
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Age = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                        Specialty = c.String(nullable: false),
                        TrainerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TrainerId)
                .Index(t => t.TrainerId);
            
            AddColumn("dbo.Courses", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Courses", "UserId");
            AddForeignKey("dbo.Courses", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainers", "TrainerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Trainees", "TraineeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Staffs", "StaffId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Trainers", new[] { "TrainerId" });
            DropIndex("dbo.Trainees", new[] { "TraineeId" });
            DropIndex("dbo.Staffs", new[] { "StaffId" });
            DropIndex("dbo.Courses", new[] { "UserId" });
            DropColumn("dbo.Courses", "UserId");
            DropTable("dbo.Trainers");
            DropTable("dbo.Trainees");
            DropTable("dbo.Staffs");
        }
    }
}
