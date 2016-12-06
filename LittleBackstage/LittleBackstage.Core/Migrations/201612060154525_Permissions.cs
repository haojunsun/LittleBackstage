namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Permissions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "Permissions", c => c.String());
            DropColumn("dbo.Roles", "RoleAuthority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "RoleAuthority", c => c.Int(nullable: false));
            DropColumn("dbo.Roles", "Permissions");
        }
    }
}
