namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        ManagerId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        PassWord = c.String(),
                        LastLoginTime = c.DateTime(),
                        Register = c.DateTime(),
                        IsEnable = c.Int(nullable: false),
                        IsExamine = c.Int(nullable: false),
                        Role_RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.ManagerId)
                .ForeignKey("dbo.Roles", t => t.Role_RoleId)
                .Index(t => t.Role_RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.Int(nullable: false),
                        RoleAuthority = c.Int(nullable: false),
                        CreateTime = c.DateTime(),
                        RoleType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.SystemLogs",
                c => new
                    {
                        SystemLogId = c.Int(nullable: false, identity: true),
                        LogType = c.Int(nullable: false),
                        LogUserName = c.String(),
                        LogUserId = c.String(),
                        OperateType = c.String(),
                        LogTime = c.DateTime(),
                        LogDetails = c.String(),
                    })
                .PrimaryKey(t => t.SystemLogId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        PassWord = c.String(),
                        LastLoginTime = c.DateTime(),
                        Register = c.DateTime(),
                        IsEnable = c.Int(nullable: false),
                        IsExamine = c.Int(nullable: false),
                        Role_RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.Role_RoleId)
                .Index(t => t.Role_RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.Managers", "Role_RoleId", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "Role_RoleId" });
            DropIndex("dbo.Managers", new[] { "Role_RoleId" });
            DropTable("dbo.Users");
            DropTable("dbo.SystemLogs");
            DropTable("dbo.Roles");
            DropTable("dbo.Managers");
        }
    }
}
