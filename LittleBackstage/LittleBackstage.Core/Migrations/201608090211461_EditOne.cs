namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditOne : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ForExcels", "LuYinHuanJin_WenDu", c => c.String());
            AddColumn("dbo.ForExcels", "LuYinHuanJin_ShiDu", c => c.String());
            AddColumn("dbo.ForExcels", "LuYinHuanJin_DaQiYa", c => c.String());
            AddColumn("dbo.ForExcels", "LuYinHuanJin_BenDiZhaoSheng", c => c.String());
            AddColumn("dbo.ForExcels", "LuYinHuanJin_LuYinPengChiCun", c => c.String());
            AddColumn("dbo.ForExcels", "LuYinHuanJin_HunXiangShiJian", c => c.String());
            AddColumn("dbo.ForExcels", "LuYinHuanJin_LuYinRuanJian", c => c.String());
            AddColumn("dbo.ForExcels", "LuYinHuanJin_LuYinSheBei", c => c.String());
            AddColumn("dbo.ForExcels", "LuYinHuanJin_ShiYinTu", c => c.String());
            AddColumn("dbo.ForExcels", "JiBenXinXi_XingBie", c => c.String());
            AddColumn("dbo.ForExcels", "JiBenXinXi_ChuShengNianYue", c => c.String());
            AddColumn("dbo.ForExcels", "JiBenXinXi_JiGuan", c => c.String());
            AddColumn("dbo.ForExcels", "JiBenXinXi_XueLi", c => c.String());
            AddColumn("dbo.ForExcels", "JiBenXinXi_ZhuanYe", c => c.String());
            AddColumn("dbo.ForExcels", "JiBenXinXi_ZhiCheng", c => c.String());
            AddColumn("dbo.ForExcels", "JiBenXinXi_XueSuoYanZouYueQiShiJian", c => c.String());
            AddColumn("dbo.ForExcels", "JiBenXinXi_LianXiFangShi", c => c.String());
            AddColumn("dbo.ForExcels", "JiBenXinXi_GongZuoDanWei", c => c.String());
            AddColumn("dbo.ForExcels", "JiBenXinXi_TingLiZhuangKuang", c => c.String());
            AddColumn("dbo.ForExcels", "JiBenXinXi_ShiChengGuanXi", c => c.String());
            AddColumn("dbo.ForExcels", "JiBenXinXi_ChuanLue", c => c.String());
            DropColumn("dbo.ForExcels", "WenDu");
            DropColumn("dbo.ForExcels", "ShiDu");
            DropColumn("dbo.ForExcels", "DaQiYa");
            DropColumn("dbo.ForExcels", "BenDiZhaoSheng");
            DropColumn("dbo.ForExcels", "LuYinPengChiCun");
            DropColumn("dbo.ForExcels", "HunXiangShiJian");
            DropColumn("dbo.ForExcels", "LuYinRuanJian");
            DropColumn("dbo.ForExcels", "LuYinSheBei");
            DropColumn("dbo.ForExcels", "ShiYinTu");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ForExcels", "ShiYinTu", c => c.String());
            AddColumn("dbo.ForExcels", "LuYinSheBei", c => c.String());
            AddColumn("dbo.ForExcels", "LuYinRuanJian", c => c.String());
            AddColumn("dbo.ForExcels", "HunXiangShiJian", c => c.String());
            AddColumn("dbo.ForExcels", "LuYinPengChiCun", c => c.String());
            AddColumn("dbo.ForExcels", "BenDiZhaoSheng", c => c.String());
            AddColumn("dbo.ForExcels", "DaQiYa", c => c.String());
            AddColumn("dbo.ForExcels", "ShiDu", c => c.String());
            AddColumn("dbo.ForExcels", "WenDu", c => c.String());
            DropColumn("dbo.ForExcels", "JiBenXinXi_ChuanLue");
            DropColumn("dbo.ForExcels", "JiBenXinXi_ShiChengGuanXi");
            DropColumn("dbo.ForExcels", "JiBenXinXi_TingLiZhuangKuang");
            DropColumn("dbo.ForExcels", "JiBenXinXi_GongZuoDanWei");
            DropColumn("dbo.ForExcels", "JiBenXinXi_LianXiFangShi");
            DropColumn("dbo.ForExcels", "JiBenXinXi_XueSuoYanZouYueQiShiJian");
            DropColumn("dbo.ForExcels", "JiBenXinXi_ZhiCheng");
            DropColumn("dbo.ForExcels", "JiBenXinXi_ZhuanYe");
            DropColumn("dbo.ForExcels", "JiBenXinXi_XueLi");
            DropColumn("dbo.ForExcels", "JiBenXinXi_JiGuan");
            DropColumn("dbo.ForExcels", "JiBenXinXi_ChuShengNianYue");
            DropColumn("dbo.ForExcels", "JiBenXinXi_XingBie");
            DropColumn("dbo.ForExcels", "LuYinHuanJin_ShiYinTu");
            DropColumn("dbo.ForExcels", "LuYinHuanJin_LuYinSheBei");
            DropColumn("dbo.ForExcels", "LuYinHuanJin_LuYinRuanJian");
            DropColumn("dbo.ForExcels", "LuYinHuanJin_HunXiangShiJian");
            DropColumn("dbo.ForExcels", "LuYinHuanJin_LuYinPengChiCun");
            DropColumn("dbo.ForExcels", "LuYinHuanJin_BenDiZhaoSheng");
            DropColumn("dbo.ForExcels", "LuYinHuanJin_DaQiYa");
            DropColumn("dbo.ForExcels", "LuYinHuanJin_ShiDu");
            DropColumn("dbo.ForExcels", "LuYinHuanJin_WenDu");
        }
    }
}
