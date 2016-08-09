using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBackstage.Core.Models
{
    public class ForExcel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ForExcelId { get; set; }

        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// 人工编码
        /// </summary>
        public string RenGongBianMa { get; set; }


        /// <summary>
        /// 题名_正题名
        /// </summary>
        public string TiMing_ZhengTiMing { get;set;}
        /// <summary>
        /// 题名_其他题名
        /// </summary>
        public string TiMing_QiTaTiMing { get; set; }

        /// <summary>
        /// 类别_演奏方式
        /// </summary>
        public string LeiBie_YanZouFangShi { get; set; }
        /// <summary>
        /// 类别_霍-萨
        /// </summary>
        public string LeiBie_HuoSa { get; set; }

        /// <summary>
        /// 乐种
        /// </summary>
        public string YueZhong { get; set; }

        /// <summary>
        /// 民族属性
        /// </summary>
        public string MinZuShuXing { get; set; }


        /// <summary>
        /// 来源_所有/持有者
        /// </summary>
        public string LaiYuan_SuoYouZhe { get; set; }

        /// <summary>
        /// 来源_征集时间
        /// </summary>
        public string LaiYuan_ZhengJiShiJian { get; set; }

        /// <summary>
        /// 来源_征集地点
        /// </summary>
        public string LaiYuan_ZhengJiDiDian { get; set; }



        /// <summary>
        /// 文化背景_历史源流
        /// </summary>
        public string WenHuaBeiJing_LiShiYuanLiu { get; set; }
        /// <summary>
        /// 文化背景_流布地域
        /// </summary>
        public string WenHuaBeiJing_LiuBuDiYu { get; set; }
        /// <summary>
        /// 文化背景_传承_制作
        /// </summary>
        public string WenHuaBeiJing_ChuanCheng_ZhiZuo { get; set; }
        /// <summary>
        //文化背景_传承_演奏
        /// </summary>
        public string WenHuaBeiJing_ChuanCheng_YanZou { get; set; }
        /// <summary>
        /// 文化背景_使用场所
        /// </summary>
        public string WenHuaBeiJing_ShiYuongChangSuo { get; set; }
        /// <summary>
        /// 文化背景_社会功能
        /// </summary>
        public string WenHuaBeiJing_SheHuiGongNeng { get; set; }



        /// <summary>
        ///乐器形制_结构示意图
        /// </summary>
        public string YueQiXingZhi_JieGouShiYiTu { get; set; }
        /// <summary>
        /// 乐器形制_发音体
        /// </summary>
        public string YueQiXingZhi_FaYinTi { get; set; }
        /// <summary>
        /// 乐器形制_共鸣体
        /// </summary>
        public string YueQiXingZhi_GongMingTi { get; set; }
        /// <summary>
        /// 乐器形制_其他
        /// </summary>
        public string YueQiXingZhi_QiTa{ get; set; }





        /// <summary>
        /// 乐器工艺_基本工艺
        /// </summary>
        public string YueQiGongYi_JiBenGongYi { get; set; }
        /// <summary>
        /// 乐器工艺_特殊工艺_漆艺
        /// </summary>
        public string YueQiGongYi_TeShuGongYi_QiYi { get; set; }
        /// <summary>
        /// 乐器工艺_特殊工艺_颜色
        /// </summary>
        public string YueQiGongYi_TeShuGongYi_YanSe { get; set; }
        /// <summary>
        /// 乐器工艺_特殊工艺_其他
        /// </summary>
        public string YueQiGongYi_TeShuGongYi_QiTa { get; set; }

        /// <summary>
        /// 乐器工艺_制作时间
        /// </summary>
        public string YueQiGongYi_ZhiZuoShiJian { get; set; }
        /// <summary>
        /// 乐器工艺_制作地点
        /// </summary>
        public string YueQiGongYi_ZhiZuoDiDian { get; set; }
        /// <summary>
        /// 乐器工艺_制作者
        /// </summary>
        public string YueQiGongYi_ZhiZuoZhe { get; set; }
        /// <summary>
        /// 乐器工艺_制作流程
        /// </summary>
        public string YueQiGongYi_ZhiZuoLiuCheng { get; set; }
        /// <summary>
        /// 乐器工艺_改良情况
        /// </summary>
        public string YueQiGongYi_GaiLiangQingKuang{ get; set; }



        /// <summary>
        /// 乐器音响_发声方式
        /// </summary>
        public string YueQiYiXiang_FaShengFangShi { get; set; }
        /// <summary>
        /// 乐器音响_音律_调高
        /// </summary>
        public string YueQiGongYi_YiLu_DiaoGao { get; set; }
        /// <summary>
        /// 乐器音响_音律_筒音
        /// </summary>
        public string YueQiGongYi_YiLu_TongYin { get; set; }
        /// <summary>
        /// 乐器音响_音律_定弦
        /// </summary>
        public string YueQiGongYi_YiLu_DingXuan { get; set; }
        /// <summary>
        /// 乐器音响_音列
        /// </summary>
        public string YueQiGongYi_YinLie { get; set; }
        /// <summary>
        /// 乐器音响_调音法
        /// </summary>
        public string YueQiGongYi_TiaoYinFa { get; set; }



        /// <summary>
        ///演奏技法_演奏技法
        /// </summary>
        public string YanZouJiFa_YanZouJiFa { get; set; }

        /// <summary>
        /// 演奏技法_示范乐曲
        /// </summary>
        public string YanZouJiFa_ShiFanYueQu { get; set; }

        /// <summary>
        /// 演奏技法_演奏者基本信息
        /// </summary>
        public string YanZouJiFa_YanZouZheJiBenXinXi { get; set; }


        /// <summary>
        /// 采录信息_采录者
        /// </summary>
        public string CaiLuXinXi_CaiLuZhe { get; set; }
        /// <summary>
        /// 采录信息_采录时间
        /// </summary>
        public string CaiLuXinXi_CaiLuShiJian { get; set; }
        /// <summary>
        /// 采录信息_采录地点
        /// </summary>
        public string CaiLuXinXi_DiDian { get; set; }
        /// <summary>
        /// 采录信息_采录环境
        /// </summary>
        public string CaiLuXinXi_CaiLuHuanJing { get; set; }
        /// <summary>
        /// 采录信息_著录者
        /// </summary>
        public string CaiLuXinXi_ZhuLuZhe { get; set; }
        /// <summary>
        /// 采录信息_采录导演
        /// </summary>
        public string CaiLuXinXi_CaiLuDaoYan { get; set; }
        /// <summary>
        /// 采录信息_版权
        /// </summary>
        public string CaiLuXinXi_BanQuan { get; set; }
        
        /// <summary>
        /// 乐器简介
        /// </summary>
        public string YueQiJianJie { get; set; }
        /// <summary>
        /// 声学测量与频谱分析报告
        /// </summary>
        public string ShengXueCeLiangYuPinPuFenXiBaoGao { get; set; }
        /// <summary>
        /// 测量信息记录表
        /// </summary>
        public string CeLiangXinXiJiLuBiao { get; set; }
        //-------------------------------------

        /// <summary>
        /// 录音环境_温度
        /// </summary>
        public string LuYinHuanJin_WenDu { get; set; }

        /// <summary>
        ///  录音环境_湿度
        /// </summary>
        public string LuYinHuanJin_ShiDu { get; set; }

        /// <summary>
        ///  录音环境_大气压
        /// </summary>
        public string LuYinHuanJin_DaQiYa { get; set; }

        /// <summary>
        ///  录音环境_本底噪声
        /// </summary>
        public string LuYinHuanJin_BenDiZhaoSheng { get; set; }

        /// <summary>
        ///  录音环境_录音棚尺寸
        /// </summary>
        public string LuYinHuanJin_LuYinPengChiCun { get; set; }

        /// <summary>
        ///  录音环境_混响时间
        /// </summary>
        public string LuYinHuanJin_HunXiangShiJian { get; set; }

        /// <summary>
        ///  录音环境_录音软件
        /// </summary>
        public string LuYinHuanJin_LuYinRuanJian { get; set; }

        /// <summary>
        ///  录音环境_录音设备
        /// </summary>
        public string LuYinHuanJin_LuYinSheBei { get; set; }

        /// <summary>
        ///  录音环境_拾音图
        /// </summary>
        public string LuYinHuanJin_ShiYinTu { get; set; }

        //-------------------------------------

        /// <summary>
        /// 发音体_名称
        /// </summary>
        public string Kaleida_MingCheng { get; set; }
        /// <summary>
        /// 发音体_结构尺寸
        /// </summary>
        public string Kaleida_JieGouChiCun { get; set; }
        /// <summary>
        /// 发音体_材质
        /// </summary>
        public string Kaleida_CaiZhi { get; set; }
        /// <summary>
        /// 发音体_重量
        /// </summary>
        public string Kaleida_ZhongLiang { get; set; }
        //-------------------------------------

        /// <summary>
        /// 共鸣体_名称
        /// </summary>
        public string Resonator_MingCheng { get; set; }
        /// <summary>
        /// 共鸣体_结构尺寸
        /// </summary>
        public string Resonator_JieGouChiCun { get; set; }
        /// <summary>
        /// 共鸣体_材质
        /// </summary>
        public string Resonator_CaiZhi { get; set; }
        /// <summary>
        /// 共鸣体_重量
        /// </summary>
        public string Resonator_ZhongLiang { get; set; }

        //-------------------------------------

        /// <summary>
        /// 其他_名称
        /// </summary>
        public string Other_MingCheng { get; set; }
        /// <summary>
        /// 其他_结构尺寸
        /// </summary>
        public string Other_JieGouChiCun { get; set; }
        /// <summary>
        /// 其他_材质
        /// </summary>
        public string Other_CaiZhi { get; set; }
        /// <summary>
        /// 其他_重量
        /// </summary>
        public string Other_ZhongLiang { get; set; }
        //-------------------------------------


        /// <summary>
        /// 1kHz
        /// </summary>
        public string YikHz { get; set; }
        /// <summary>
        /// 空弦音/筒音_自然衰减、力度p 第一遍
        /// </summary>
        public string KongXianYin_ZiRanShuaiJian_P_First { get; set; }
        /// <summary>
        /// 空弦音/筒音_自然衰减、力度p 第二遍
        /// </summary>
        public string KongXianYin_ZiRanShuaiJian_P_Second { get; set; }
        /// <summary>
        /// 空弦音/筒音_自然衰减、力度f 第一遍
        /// </summary>
        public string KongXianYin_ZiRanShuaiJian_F_First { get; set; }
        /// <summary>
        /// 空弦音/筒音_自然衰减、力度f 第二遍
        /// </summary>
        public string KongXianYin_ZiRanShuaiJian_F_Second { get; set; }
        /// <summary>
        /// 空弦音/筒音_自然衰减、力度mf 第一遍
        /// </summary>
        public string KongXianYin_ZiRanShuaiJian_MF_First { get; set; }
        /// <summary>
        /// 空弦音/筒音_自然衰减、力度f 第二遍
        /// </summary>
        public string KongXianYin_ZiRanShuaiJian_MF_Second { get; set; }

        //-------------------------------------



        /// <summary>
        /// 音阶_自然衰减、力度mf 第一遍
        /// </summary>
        public string YinJie_ZiRanShuaiJian_MF_First { get; set; }
        /// <summary>
        /// 音阶_自然衰减、力度f 第二遍
        /// </summary>
        public string YinJie_ZiRanShuaiJian_MF_Second { get; set; }
        /// <summary>
        /// 音阶_慢速、力度p 第一遍
        /// </summary>
        public string YinJie_ManSu_P_First { get; set; }
        /// <summary>
        /// 音阶_慢速、力度p 第二遍
        /// </summary>
        public string YinJie_ManSu_P_Second { get; set; }
        /// <summary>
        /// 音阶_慢速、力度f 第一遍
        /// </summary>
        public string YinJie_ManSu_F_First { get; set; }
        /// <summary>
        /// 音阶_慢速、力度f 第二遍
        /// </summary>
        public string YinJie_ManSu_F_Second { get; set; }
        /// <summary>
        /// 音阶_慢速、力度mf 第一遍
        /// </summary>
        public string YinJie_ManSu_MF_First { get; set; }
        /// <summary>
        /// 音阶_慢速、力度mf 第二遍
        /// </summary>
        public string YinJie_ManSu_MF_Second { get; set; }
        /// <summary>
        /// 音阶_中速、力度f 第一遍
        /// </summary>
        public string YinJie_ZhongSu_F_First { get; set; }
        /// <summary>
        /// 音阶_中速、力度f 第二遍
        /// </summary>
        public string YinJie_ZhongSu_F_Second { get; set; }

        //-------------------------------------
        /// <summary>
        /// 演奏技法_技法名称
        /// </summary>
        public string YanZhouJiFa_MingChen { get; set; }
        /// <summary>
        /// 演奏技法_第一遍
        /// </summary>
        public string YanZhouJiFa_First { get; set; }
        /// <summary>
        /// 演奏技法_第二遍
        /// </summary>
        public string YanZhouJiFa_Second { get; set; }

        //-------------------------------------
        /// <summary>
        /// 示范乐曲_曲目名
        /// </summary>
        public string ShiFanYuQu_QuMuMing { get; set; }
        /// <summary>
        /// 示范乐曲_音频
        /// </summary>
        public string ShiFanYuQu_YinPin { get; set; }
        /// <summary>
        /// 示范乐曲_视频
        /// </summary>
        public string ShiFanYuQu_ShiPin { get; set; }

        //-------------------------------------

        /// <summary>
        /// 基本信息_性别 
        /// </summary>
        public string JiBenXinXi_XingBie { get; set; }
        /// <summary>
        /// 基本信息_出生年月
        /// </summary>
        public string JiBenXinXi_ChuShengNianYue { get; set; }
        /// <summary>
        /// 基本信息_籍贯
        /// </summary>
        public string JiBenXinXi_JiGuan { get; set; }
        /// <summary>
        /// 基本信息_学历
        /// </summary>
        public string JiBenXinXi_XueLi { get; set; }
        /// <summary>
        /// 基本信息_专业/职业
        /// </summary>
        public string JiBenXinXi_ZhuanYe { get; set; }
        /// <summary>
        /// 基本信息_职称
        /// </summary>
        public string JiBenXinXi_ZhiCheng { get; set; }
        /// <summary>
        /// 基本信息_学所演奏乐器时间
        /// </summary>
        public string JiBenXinXi_XueSuoYanZouYueQiShiJian { get; set; }
        /// <summary>
        /// 基本信息_联系方式
        /// </summary>
        public string JiBenXinXi_LianXiFangShi { get; set; }
        /// <summary>
        /// 基本信息_工作单位 
        /// </summary>
        public string JiBenXinXi_GongZuoDanWei { get; set; }
        /// <summary>
        /// 基本信息_听力状况
        /// </summary>
        public string JiBenXinXi_TingLiZhuangKuang { get; set; }
        /// <summary>
        /// 基本信息_师承关系
        /// </summary>
        public string JiBenXinXi_ShiChengGuanXi { get; set; }
        /// <summary>
        /// 基本信息_传略
        /// </summary>
        public string JiBenXinXi_ChuanLue { get; set; }


    }
}
