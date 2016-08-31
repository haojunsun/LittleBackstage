using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBackstage.Core.Models
{
    public class LiteratureExcel
    {
        public int LiteratureExcelId { get; set; }

        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// 系统编码
        /// </summary>
        public string InventoryId { get; set; }

        /// <summary>
        /// 人工编码
        /// </summary>
        public string ArtificialId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string TitleProper { get; set; }

        /// <summary>
        /// 项目名称 
        /// </summary>
        public string XiangMuMingCheng { get; set; }

        /// <summary>
        /// 一级分类
        /// </summary>
        public string FirstLevel { get; set; }

        /// <summary>
        /// 二级分类
        /// </summary>
        public string SecondLevel { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string XiangMuBianMa { get; set; }
        /// <summary>
        /// 子项序号
        /// </summary>
        public string ZiXiangXuHao { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string PiCi { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string Annotation { get; set; }
        /// <summary>
        /// 项目申请时间
        /// </summary>
        public string XiangMuShenQingShiJian { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string MinZu { get; set; }

        /// <summary>
        /// 支系
        /// </summary>
        public string ZhiXi { get; set; }
        /// <summary>
        /// 项目简介标准版
        /// </summary>
        public string XiangMuJianJieBiaoZhunBan { get; set; }
        /// <summary>
        /// 项目简介
        /// </summary>
        public string XiangMuJianJie { get; set; }
        /// <summary>
        /// 所在区域及其地理环境
        /// </summary>
        public string SuoZaiQuYuJiQiDiLiHuanJing { get; set; }
        /// <summary>
        /// 分布区域
        /// </summary>
        public string FenBuQuYu { get; set; }

        /// <summary>
        /// 历史渊源
        /// </summary>
        public string LiShiYuanYuan { get; set; }
        /// <summary>
        /// 基本内容
        /// </summary>
        public string JiBenNeiRong { get; set; }
        /// <summary>
        /// 相关制品及其作品
        /// </summary>
        public string XiangGuanZhiPinJiQiZuoPin { get; set; }
        /// <summary>
        /// 传承谱系
        /// </summary>
        public string ChuanChengPuXi { get; set; }
        /// <summary>
        /// 代表性传承人
        /// </summary>
        public string DaiBiaoXingChuanChengRen { get; set; }

        /// <summary>
        /// 主要特征
        /// </summary>
        public string ZhuYaoTeZheng { get; set; }
        /// <summary>
        /// 重要价值
        /// </summary>
        public string ZhongYaoJiaZhi { get; set; }
        /// <summary>
        /// 存续状况
        /// </summary>
        public string CunXuZhuangKuang { get; set; }
        /// <summary>
        /// 主要传承人（群体）
        /// </summary>
        public string ZhuYaoChuanChengRen { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 一级：省/直辖市/自治区Province/Municipality
        /// </summary>
        public string CoverageSpatial_Province { get; set; }
        /// <summary>
        /// 二级：市/地区City
        /// </summary>
        public string CoverageSpatial_City { get; set; }
        /// <summary>
        /// 三级：区/县/县级市County
        /// </summary>
        public string CoverageSpatial_County { get; set; }

        /// <summary>
        /// 申报地区或单位
        /// </summary>
        public string CoverageSpatial_ShenBaoDiQuHuoDanWei { get; set; }
        /// <summary>
        /// 保护单位
        /// </summary>
        public string CoverageSpatial_BaoHuDanWei { get; set; }
        /// <summary>
        /// 生态区项目
        /// </summary>
        public string CoverageSpatial_ShengTaiQuXiangMu { get; set; }
        /// <summary>
        /// 生产性保护示范基地
        /// </summary>
        public string CoverageSpatial_ShengChanXingBaoHuShiFanJiDi{ get; set; }

        /// <summary>
        /// 来源 源资料创建者 名称
        /// </summary>
        public string Source_CreatorOfReferences_Name { get; set; }

        /// <summary>
        /// 来源 源资料创建者 责任方式 
        /// </summary>
        public string Source_CreatorOfReferences_Role { get; set; }

        /// <summary>
        /// 源资料提供者
        /// </summary>
        public string Source_ProviderOfReferences { get; set; }

        /// <summary>
        /// 源资料典藏单位Repository Name
        /// </summary>
        public string Source_RepositoryName { get; set; }

        /// <summary>
        /// 数字化格式
        /// </summary>
        public string DigitalObjectInformation_DigitalObjectFormat { get; set; }
        /// <summary>
        /// 大小Size
        /// </summary>
        public string DigitalObjectInformation_Size { get; set; }
        /// <summary>
        /// 时长
        /// </summary>
        public string DigitalObjectInformation_Duration { get; set; }

        /// <summary>
        /// 分辨率（解析度）
        /// </summary>
        public string DigitalObjectInformation_DigitalSpecification { get; set; }
        /// <summary>
        /// 音/视频数据码率
        /// </summary>
        public string DigitalObjectInformation_AudioVideo { get; set; }
        /// <summary>
        /// 音频采样频率Audio Sampling Frequency 
        /// </summary>
        public string DigitalObjectInformation_AudioSamplingFrequency  { get; set; }
        /// <summary>
        /// 声道数Number Of Channels
        /// </summary>
        public string DigitalObjectInformation_NumberOfChannels { get; set; }

        /// <summary>
        /// 储存地址File Path
        /// </summary>
        public string DigitalObjectInformation_FilePath { get; set; }
        /// <summary>
        /// 显示级别Display Type
        /// </summary>
        public string DigitalObjectInformation_DisplayType { get; set; }

        /// <summary>
        /// 著录者
        /// </summary>
        public string RecordingInformation_Recorder { get; set; }
        /// <summary>
        /// 著录时间
        /// </summary>
        public string RecordingInformation_RecordingTime { get; set; }
        /// <summary>
        /// 审核者
        /// </summary>
        public string RecordingInformation_MetadataAuditor { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public string RecordingInformation_MetadataAuditoTime { get; set; }
        /// <summary>
        /// 管理机构
        /// </summary>
        public string RecordingInformation_MetadataManagement { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string RecordingInformation_Note { get; set; }
        public string Other1 { get; set; }
        public string Other2 { get; set; }
        public string Other3 { get; set; }
        public string Other4 { get; set; }
        public string Other5 { get; set; }
    }
    public class JsonData
    {
        public List<LiteratureExcel> list { get; set; }
        public int totalCount { get; set; }
    }
}
