namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNickName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Phone", c => c.String());
            AddColumn("dbo.Users", "NickName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "NickName");
            DropColumn("dbo.Users", "Phone");
        }
    }
}
