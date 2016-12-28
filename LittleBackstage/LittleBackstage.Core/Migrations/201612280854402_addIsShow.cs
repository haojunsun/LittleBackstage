namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsShow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoryFields", "IsShow", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CategoryFields", "IsShow");
        }
    }
}
