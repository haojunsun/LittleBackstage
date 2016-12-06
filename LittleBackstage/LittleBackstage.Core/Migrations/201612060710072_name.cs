namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class name : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Roles", "RoleName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Roles", "RoleName", c => c.Int(nullable: false));
        }
    }
}
