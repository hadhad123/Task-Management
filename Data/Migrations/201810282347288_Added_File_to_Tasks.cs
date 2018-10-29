namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_File_to_Tasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Task", "File", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Task", "File");
        }
    }
}
