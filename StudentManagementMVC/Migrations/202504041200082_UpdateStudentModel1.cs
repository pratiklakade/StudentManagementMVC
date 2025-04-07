namespace StudentManagementMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStudentModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Course", c => c.String(nullable: false));
            AddColumn("dbo.Students", "Semester", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Semester");
            DropColumn("dbo.Students", "Course");
        }
    }
}
