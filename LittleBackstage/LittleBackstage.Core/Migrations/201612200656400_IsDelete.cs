namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IsDelete", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "IsDelete");
        }
    }
}
