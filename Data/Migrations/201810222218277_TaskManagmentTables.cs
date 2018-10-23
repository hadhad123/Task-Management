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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Task", t => t.TaskID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.TaskID);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                        StatusID = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        AssignedTo = c.Int(nullable: false),
                        User_ID = c.Int(),
                        AssignedUser_ID = c.Int(),
                        CreatedUser_ID = c.Int(),
                        TaskStatus_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.User_ID)
                .ForeignKey("dbo.User", t => t.AssignedUser_ID)
                .ForeignKey("dbo.User", t => t.CreatedUser_ID)
                .ForeignKey("dbo.TaskStatus", t => t.TaskStatus_ID)
                .Index(t => t.User_ID)
                .Index(t => t.AssignedUser_ID)
                .Index(t => t.CreatedUser_ID)
                .Index(t => t.TaskStatus_ID);
            
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
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
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
            DropForeignKey("dbo.Comment", "TaskID", "dbo.Task");
            DropForeignKey("dbo.Task", "TaskStatus_ID", "dbo.TaskStatus");
            DropForeignKey("dbo.Task", "CreatedUser_ID", "dbo.User");
            DropForeignKey("dbo.Task", "AssignedUser_ID", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserID", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleID", "dbo.Role");
            DropForeignKey("dbo.Task", "User_ID", "dbo.User");
            DropIndex("dbo.UserRole", new[] { "RoleID" });
            DropIndex("dbo.UserRole", new[] { "UserID" });
            DropIndex("dbo.Task", new[] { "TaskStatus_ID" });
            DropIndex("dbo.Task", new[] { "CreatedUser_ID" });
            DropIndex("dbo.Task", new[] { "AssignedUser_ID" });
            DropIndex("dbo.Task", new[] { "User_ID" });
            DropIndex("dbo.Comment", new[] { "TaskID" });
            DropIndex("dbo.Comment", new[] { "UserID" });
            DropTable("dbo.TaskStatus");
            DropTable("dbo.Role");
            DropTable("dbo.UserRole");
            DropTable("dbo.User");
            DropTable("dbo.Task");
            DropTable("dbo.Comment");
        }
    }
}
