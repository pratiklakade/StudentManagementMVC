namespace StudentManagementMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStudentModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "FullName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Students", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Email", c => c.String());
            AlterColumn("dbo.Students", "FullName", c => c.String(nullable: false));
        }
    }
}
