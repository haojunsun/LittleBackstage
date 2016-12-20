namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CanModify : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoryFields", "CanModify", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CategoryFields", "CanModify");
        }
    }
}
