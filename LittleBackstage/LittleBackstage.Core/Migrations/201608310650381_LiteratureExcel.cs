namespace LittleBackstage.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LiteratureExcel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LiteratureExcels",
                c => new
                    {
                        LiteratureExcelId = c.Int(nullable: false, identity: true),
                        CreatedUtc = c.DateTime(nullable: false),
                        InventoryId = c.String(),
                        ArtificialId = c.String(),
                        TitleProper = c.String(),
                        XiangMuMingCheng = c.String(),
                        FirstLevel = c.String(),
                        SecondLevel = c.String(),
                        XiangMuBianMa = c.String(),
                        ZiXiangXuHao = c.String(),
                        PiCi = c.String(),
                        Annotation = c.String(),
                        XiangMuShenQingShiJian = c.String(),
                        MinZu = c.String(),
                        ZhiXi = c.String(),
                        XiangMuJianJieBiaoZhunBan = c.String(),
                        XiangMuJianJie = c.String(),
                        SuoZaiQuYuJiQiDiLiHuanJing = c.String(),
                        FenBuQuYu = c.String(),
                        LiShiYuanYuan = c.String(),
                        JiBenNeiRong = c.String(),
                        XiangGuanZhiPinJiQiZuoPin = c.String(),
                        ChuanChengPuXi = c.String(),
                        DaiBiaoXingChuanChengRen = c.String(),
                        ZhuYaoTeZheng = c.String(),
                        ZhongYaoJiaZhi = c.String(),
                        CunXuZhuangKuang = c.String(),
                        ZhuYaoChuanChengRen = c.String(),
                        Type = c.String(),
                        CoverageSpatial_Province = c.String(),
                        CoverageSpatial_City = c.String(),
                        CoverageSpatial_County = c.String(),
                        CoverageSpatial_ShenBaoDiQuHuoDanWei = c.String(),
                        CoverageSpatial_BaoHuDanWei = c.String(),
                        CoverageSpatial_ShengTaiQuXiangMu = c.String(),
                        CoverageSpatial_ShengChanXingBaoHuShiFanJiDi = c.String(),
                        Source_CreatorOfReferences_Name = c.String(),
                        Source_CreatorOfReferences_Role = c.String(),
                        Source_ProviderOfReferences = c.String(),
                        Source_RepositoryName = c.String(),
                        DigitalObjectInformation_DigitalObjectFormat = c.String(),
                        DigitalObjectInformation_Size = c.String(),
                        DigitalObjectInformation_Duration = c.String(),
                        DigitalObjectInformation_DigitalSpecification = c.String(),
                        DigitalObjectInformation_AudioVideo = c.String(),
                        DigitalObjectInformation_AudioSamplingFrequency = c.String(),
                        DigitalObjectInformation_NumberOfChannels = c.String(),
                        DigitalObjectInformation_FilePath = c.String(),
                        DigitalObjectInformation_DisplayType = c.String(),
                        RecordingInformation_Recorder = c.String(),
                        RecordingInformation_RecordingTime = c.String(),
                        RecordingInformation_MetadataAuditor = c.String(),
                        RecordingInformation_MetadataAuditoTime = c.String(),
                        RecordingInformation_MetadataManagement = c.String(),
                        RecordingInformation_Note = c.String(),
                        Other1 = c.String(),
                        Other2 = c.String(),
                        Other3 = c.String(),
                        Other4 = c.String(),
                        Other5 = c.String(),
                    })
                .PrimaryKey(t => t.LiteratureExcelId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LiteratureExcels");
        }
    }
}
