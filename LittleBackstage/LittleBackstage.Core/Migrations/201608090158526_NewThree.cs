namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewThree : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ForExcels", "YikHz", c => c.String());
            AddColumn("dbo.ForExcels", "KongXianYin_ZiRanShuaiJian_P_First", c => c.String());
            AddColumn("dbo.ForExcels", "KongXianYin_ZiRanShuaiJian_P_Second", c => c.String());
            AddColumn("dbo.ForExcels", "KongXianYin_ZiRanShuaiJian_F_First", c => c.String());
            AddColumn("dbo.ForExcels", "KongXianYin_ZiRanShuaiJian_F_Second", c => c.String());
            AddColumn("dbo.ForExcels", "KongXianYin_ZiRanShuaiJian_MF_First", c => c.String());
            AddColumn("dbo.ForExcels", "KongXianYin_ZiRanShuaiJian_MF_Second", c => c.String());
            AddColumn("dbo.ForExcels", "YinJie_ZiRanShuaiJian_MF_First", c => c.String());
            AddColumn("dbo.ForExcels", "YinJie_ZiRanShuaiJian_MF_Second", c => c.String());
            AddColumn("dbo.ForExcels", "YinJie_ManSu_P_First", c => c.String());
            AddColumn("dbo.ForExcels", "YinJie_ManSu_P_Second", c => c.String());
            AddColumn("dbo.ForExcels", "YinJie_ManSu_F_First", c => c.String());
            AddColumn("dbo.ForExcels", "YinJie_ManSu_F_Second", c => c.String());
            AddColumn("dbo.ForExcels", "YinJie_ManSu_MF_First", c => c.String());
            AddColumn("dbo.ForExcels", "YinJie_ManSu_MF_Second", c => c.String());
            AddColumn("dbo.ForExcels", "YinJie_ZhongSu_F_First", c => c.String());
            AddColumn("dbo.ForExcels", "YinJie_ZhongSu_F_Second", c => c.String());
            AddColumn("dbo.ForExcels", "YanZhouJiFa_MingChen", c => c.String());
            AddColumn("dbo.ForExcels", "YanZhouJiFa_First", c => c.String());
            AddColumn("dbo.ForExcels", "YanZhouJiFa_Second", c => c.String());
            AddColumn("dbo.ForExcels", "ShiFanYuQu_QuMuMing", c => c.String());
            AddColumn("dbo.ForExcels", "ShiFanYuQu_YinPin", c => c.String());
            AddColumn("dbo.ForExcels", "ShiFanYuQu_ShiPin", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ForExcels", "ShiFanYuQu_ShiPin");
            DropColumn("dbo.ForExcels", "ShiFanYuQu_YinPin");
            DropColumn("dbo.ForExcels", "ShiFanYuQu_QuMuMing");
            DropColumn("dbo.ForExcels", "YanZhouJiFa_Second");
            DropColumn("dbo.ForExcels", "YanZhouJiFa_First");
            DropColumn("dbo.ForExcels", "YanZhouJiFa_MingChen");
            DropColumn("dbo.ForExcels", "YinJie_ZhongSu_F_Second");
            DropColumn("dbo.ForExcels", "YinJie_ZhongSu_F_First");
            DropColumn("dbo.ForExcels", "YinJie_ManSu_MF_Second");
            DropColumn("dbo.ForExcels", "YinJie_ManSu_MF_First");
            DropColumn("dbo.ForExcels", "YinJie_ManSu_F_Second");
            DropColumn("dbo.ForExcels", "YinJie_ManSu_F_First");
            DropColumn("dbo.ForExcels", "YinJie_ManSu_P_Second");
            DropColumn("dbo.ForExcels", "YinJie_ManSu_P_First");
            DropColumn("dbo.ForExcels", "YinJie_ZiRanShuaiJian_MF_Second");
            DropColumn("dbo.ForExcels", "YinJie_ZiRanShuaiJian_MF_First");
            DropColumn("dbo.ForExcels", "KongXianYin_ZiRanShuaiJian_MF_Second");
            DropColumn("dbo.ForExcels", "KongXianYin_ZiRanShuaiJian_MF_First");
            DropColumn("dbo.ForExcels", "KongXianYin_ZiRanShuaiJian_F_Second");
            DropColumn("dbo.ForExcels", "KongXianYin_ZiRanShuaiJian_F_First");
            DropColumn("dbo.ForExcels", "KongXianYin_ZiRanShuaiJian_P_Second");
            DropColumn("dbo.ForExcels", "KongXianYin_ZiRanShuaiJian_P_First");
            DropColumn("dbo.ForExcels", "YikHz");
        }
    }
}
