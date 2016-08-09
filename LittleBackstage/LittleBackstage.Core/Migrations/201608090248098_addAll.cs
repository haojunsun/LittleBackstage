namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAll : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ForExcels", "TiMing_ZhengTiMing", c => c.String());
            AddColumn("dbo.ForExcels", "TiMing_QiTaTiMing", c => c.String());
            AddColumn("dbo.ForExcels", "LeiBie_YanZouFangShi", c => c.String());
            AddColumn("dbo.ForExcels", "LeiBie_HuoSa", c => c.String());
            AddColumn("dbo.ForExcels", "YueZhong", c => c.String());
            AddColumn("dbo.ForExcels", "MinZuShuXing", c => c.String());
            AddColumn("dbo.ForExcels", "LaiYuan_SuoYouZhe", c => c.String());
            AddColumn("dbo.ForExcels", "LaiYuan_ZhengJiShiJian", c => c.String());
            AddColumn("dbo.ForExcels", "LaiYuan_ZhengJiDiDian", c => c.String());
            AddColumn("dbo.ForExcels", "WenHuaBeiJing_LiShiYuanLiu", c => c.String());
            AddColumn("dbo.ForExcels", "WenHuaBeiJing_LiuBuDiYu", c => c.String());
            AddColumn("dbo.ForExcels", "WenHuaBeiJing_ChuanCheng_ZhiZuo", c => c.String());
            AddColumn("dbo.ForExcels", "WenHuaBeiJing_ChuanCheng_YanZou", c => c.String());
            AddColumn("dbo.ForExcels", "WenHuaBeiJing_ShiYuongChangSuo", c => c.String());
            AddColumn("dbo.ForExcels", "WenHuaBeiJing_SheHuiGongNeng", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiXingZhi_JieGouShiYiTu", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiXingZhi_FaYinTi", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiXingZhi_GongMingTi", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiXingZhi_QiTa", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiGongYi_JiBenGongYi", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiGongYi_TeShuGongYi_QiYi", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiGongYi_TeShuGongYi_YanSe", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiGongYi_TeShuGongYi_QiTa", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiGongYi_ZhiZuoShiJian", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiGongYi_ZhiZuoDiDian", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiGongYi_ZhiZuoZhe", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiGongYi_ZhiZuoLiuCheng", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiGongYi_GaiLiangQingKuang", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiYiXiang_FaShengFangShi", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiGongYi_YiLu_DiaoGao", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiGongYi_YiLu_TongYin", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiGongYi_YiLu_DingXuan", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiGongYi_YinLie", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiGongYi_TiaoYinFa", c => c.String());
            AddColumn("dbo.ForExcels", "YanZouJiFa_YanZouJiFa", c => c.String());
            AddColumn("dbo.ForExcels", "YanZouJiFa_ShiFanYueQu", c => c.String());
            AddColumn("dbo.ForExcels", "YanZouJiFa_YanZouZheJiBenXinXi", c => c.String());
            AddColumn("dbo.ForExcels", "CaiLuXinXi_CaiLuZhe", c => c.String());
            AddColumn("dbo.ForExcels", "CaiLuXinXi_CaiLuShiJian", c => c.String());
            AddColumn("dbo.ForExcels", "CaiLuXinXi_DiDian", c => c.String());
            AddColumn("dbo.ForExcels", "CaiLuXinXi_CaiLuHuanJing", c => c.String());
            AddColumn("dbo.ForExcels", "CaiLuXinXi_ZhuLuZhe", c => c.String());
            AddColumn("dbo.ForExcels", "CaiLuXinXi_CaiLuDaoYan", c => c.String());
            AddColumn("dbo.ForExcels", "CaiLuXinXi_BanQuan", c => c.String());
            AddColumn("dbo.ForExcels", "YueQiJianJie", c => c.String());
            AddColumn("dbo.ForExcels", "ShengXueCeLiangYuPinPuFenXiBaoGao", c => c.String());
            AddColumn("dbo.ForExcels", "CeLiangXinXiJiLuBiao", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ForExcels", "CeLiangXinXiJiLuBiao");
            DropColumn("dbo.ForExcels", "ShengXueCeLiangYuPinPuFenXiBaoGao");
            DropColumn("dbo.ForExcels", "YueQiJianJie");
            DropColumn("dbo.ForExcels", "CaiLuXinXi_BanQuan");
            DropColumn("dbo.ForExcels", "CaiLuXinXi_CaiLuDaoYan");
            DropColumn("dbo.ForExcels", "CaiLuXinXi_ZhuLuZhe");
            DropColumn("dbo.ForExcels", "CaiLuXinXi_CaiLuHuanJing");
            DropColumn("dbo.ForExcels", "CaiLuXinXi_DiDian");
            DropColumn("dbo.ForExcels", "CaiLuXinXi_CaiLuShiJian");
            DropColumn("dbo.ForExcels", "CaiLuXinXi_CaiLuZhe");
            DropColumn("dbo.ForExcels", "YanZouJiFa_YanZouZheJiBenXinXi");
            DropColumn("dbo.ForExcels", "YanZouJiFa_ShiFanYueQu");
            DropColumn("dbo.ForExcels", "YanZouJiFa_YanZouJiFa");
            DropColumn("dbo.ForExcels", "YueQiGongYi_TiaoYinFa");
            DropColumn("dbo.ForExcels", "YueQiGongYi_YinLie");
            DropColumn("dbo.ForExcels", "YueQiGongYi_YiLu_DingXuan");
            DropColumn("dbo.ForExcels", "YueQiGongYi_YiLu_TongYin");
            DropColumn("dbo.ForExcels", "YueQiGongYi_YiLu_DiaoGao");
            DropColumn("dbo.ForExcels", "YueQiYiXiang_FaShengFangShi");
            DropColumn("dbo.ForExcels", "YueQiGongYi_GaiLiangQingKuang");
            DropColumn("dbo.ForExcels", "YueQiGongYi_ZhiZuoLiuCheng");
            DropColumn("dbo.ForExcels", "YueQiGongYi_ZhiZuoZhe");
            DropColumn("dbo.ForExcels", "YueQiGongYi_ZhiZuoDiDian");
            DropColumn("dbo.ForExcels", "YueQiGongYi_ZhiZuoShiJian");
            DropColumn("dbo.ForExcels", "YueQiGongYi_TeShuGongYi_QiTa");
            DropColumn("dbo.ForExcels", "YueQiGongYi_TeShuGongYi_YanSe");
            DropColumn("dbo.ForExcels", "YueQiGongYi_TeShuGongYi_QiYi");
            DropColumn("dbo.ForExcels", "YueQiGongYi_JiBenGongYi");
            DropColumn("dbo.ForExcels", "YueQiXingZhi_QiTa");
            DropColumn("dbo.ForExcels", "YueQiXingZhi_GongMingTi");
            DropColumn("dbo.ForExcels", "YueQiXingZhi_FaYinTi");
            DropColumn("dbo.ForExcels", "YueQiXingZhi_JieGouShiYiTu");
            DropColumn("dbo.ForExcels", "WenHuaBeiJing_SheHuiGongNeng");
            DropColumn("dbo.ForExcels", "WenHuaBeiJing_ShiYuongChangSuo");
            DropColumn("dbo.ForExcels", "WenHuaBeiJing_ChuanCheng_YanZou");
            DropColumn("dbo.ForExcels", "WenHuaBeiJing_ChuanCheng_ZhiZuo");
            DropColumn("dbo.ForExcels", "WenHuaBeiJing_LiuBuDiYu");
            DropColumn("dbo.ForExcels", "WenHuaBeiJing_LiShiYuanLiu");
            DropColumn("dbo.ForExcels", "LaiYuan_ZhengJiDiDian");
            DropColumn("dbo.ForExcels", "LaiYuan_ZhengJiShiJian");
            DropColumn("dbo.ForExcels", "LaiYuan_SuoYouZhe");
            DropColumn("dbo.ForExcels", "MinZuShuXing");
            DropColumn("dbo.ForExcels", "YueZhong");
            DropColumn("dbo.ForExcels", "LeiBie_HuoSa");
            DropColumn("dbo.ForExcels", "LeiBie_YanZouFangShi");
            DropColumn("dbo.ForExcels", "TiMing_QiTaTiMing");
            DropColumn("dbo.ForExcels", "TiMing_ZhengTiMing");
        }
    }
}
