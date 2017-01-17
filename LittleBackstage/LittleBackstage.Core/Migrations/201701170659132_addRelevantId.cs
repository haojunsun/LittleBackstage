namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRelevantId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SystemLogs", "RelevantId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SystemLogs", "RelevantId");
        }
    }
}
