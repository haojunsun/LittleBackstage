namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryFields : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryFields",
                c => new
                    {
                        CategoryFieldId = c.Int(nullable: false, identity: true),
                        FieldName = c.String(),
                        IdEntity = c.String(),
                        Explain = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Category_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryFieldId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .Index(t => t.Category_CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryFields", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.CategoryFields", new[] { "Category_CategoryId" });
            DropTable("dbo.CategoryFields");
        }
    }
}
