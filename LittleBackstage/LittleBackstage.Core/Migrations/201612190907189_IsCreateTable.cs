namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsCreateTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IsCreateTable", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "IsCreateTable");
        }
    }
}
