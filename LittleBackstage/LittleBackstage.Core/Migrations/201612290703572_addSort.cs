namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSort : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoryFields", "Sort", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CategoryFields", "Sort");
        }
    }
}
