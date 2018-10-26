namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskManagmentTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CommentDescription = c.String(nullable: false, maxLength: 250),
                        UserID = c.Int(nullable: false),
                        TaskID = c.Int(nullable: false),
                        ParentCommentID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Comment", t => t.ParentCommentID)
                .ForeignKey("dbo.Task", t => t.TaskID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.TaskID)
                .Index(t => t.ParentCommentID);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                        TaskStatusID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        AssignedUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID)
                .ForeignKey("dbo.User", t => t.AssignedUserID)
                .ForeignKey("dbo.TaskStatus", t => t.TaskStatusID)
                .Index(t => t.TaskStatusID)
                .Index(t => t.UserID)
                .Index(t => t.AssignedUserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 200),
                        HashedPassword = c.String(nullable: false, maxLength: 200),
                        Salt = c.String(nullable: false, maxLength: 200),
                        RoleID = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.RoleID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TaskStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "UserID", "dbo.User");
            DropForeignKey("dbo.Task", "TaskStatusID", "dbo.TaskStatus");
            DropForeignKey("dbo.Comment", "TaskID", "dbo.Task");
            DropForeignKey("dbo.Task", "AssignedUserID", "dbo.User");
            DropForeignKey("dbo.User", "RoleID", "dbo.Role");
            DropForeignKey("dbo.Task", "UserID", "dbo.User");
            DropForeignKey("dbo.Comment", "ParentCommentID", "dbo.Comment");
            DropIndex("dbo.User", new[] { "RoleID" });
            DropIndex("dbo.Task", new[] { "AssignedUserID" });
            DropIndex("dbo.Task", new[] { "UserID" });
            DropIndex("dbo.Task", new[] { "TaskStatusID" });
            DropIndex("dbo.Comment", new[] { "ParentCommentID" });
            DropIndex("dbo.Comment", new[] { "TaskID" });
            DropIndex("dbo.Comment", new[] { "UserID" });
            DropTable("dbo.TaskStatus");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Task");
            DropTable("dbo.Comment");
        }
    }
}
