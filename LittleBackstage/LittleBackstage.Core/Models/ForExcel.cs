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
        /// 温度
        /// </summary>
        public string WenDu { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        public string ShiDu { get; set; }

        /// <summary>
        /// 大气压
        /// </summary>
        public string DaQiYa { get; set; }

        /// <summary>
        /// 本底噪声
        /// </summary>
        public string BenDiZhaoSheng { get; set; }

        /// <summary>
        /// 录音棚尺寸
        /// </summary>
        public string LuYinPengChiCun { get; set; }

        /// <summary>
        /// 混响时间
        /// </summary>
        public string HunXiangShiJian { get; set; }

        /// <summary>
        /// 录音软件
        /// </summary>
        public string LuYinRuanJian { get; set; }

        /// <summary>
        /// 录音设备
        /// </summary>
        public string LuYinSheBei { get; set; }

        /// <summary>
        /// 拾音图
        /// </summary>
        public string ShiYinTu { get; set; }


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
    }
}
