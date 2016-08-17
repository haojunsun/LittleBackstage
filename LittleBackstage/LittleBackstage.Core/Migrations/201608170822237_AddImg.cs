namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ForExcels", "Img", c => c.String());
            AddColumn("dbo.ForExcels", "Other", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ForExcels", "Other");
            DropColumn("dbo.ForExcels", "Img");
        }
    }
}
