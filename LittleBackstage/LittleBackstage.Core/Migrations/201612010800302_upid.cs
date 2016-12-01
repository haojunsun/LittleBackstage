namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SystemLogs", "LogUserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SystemLogs", "LogUserId", c => c.String());
        }
    }
}
