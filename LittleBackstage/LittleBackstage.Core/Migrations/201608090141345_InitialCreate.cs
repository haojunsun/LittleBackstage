namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ForExcels",
                c => new
                    {
                        ForExcelId = c.Int(nullable: false, identity: true),
                        CreatedUtc = c.DateTime(nullable: false),
                        RenGongBianMa = c.String(),
                        WenDu = c.String(),
                        ShiDu = c.String(),
                        DaQiYa = c.String(),
                        BenDiZhaoSheng = c.String(),
                        LuYinPengChiCun = c.String(),
                        HunXiangShiJian = c.String(),
                        LuYinRuanJian = c.String(),
                        LuYinSheBei = c.String(),
                        ShiYinTu = c.String(),
                        Kaleida_MingCheng = c.String(),
                        Kaleida_JieGouChiCun = c.String(),
                        Kaleida_CaiZhi = c.String(),
                        Kaleida_ZhongLiang = c.String(),
                        Resonator_MingCheng = c.String(),
                        Resonator_JieGouChiCun = c.String(),
                        Resonator_CaiZhi = c.String(),
                        Resonator_ZhongLiang = c.String(),
                        Other_MingCheng = c.String(),
                        Other_JieGouChiCun = c.String(),
                        Other_CaiZhi = c.String(),
                        Other_ZhongLiang = c.String(),
                    })
                .PrimaryKey(t => t.ForExcelId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ForExcels");
        }
    }
}
