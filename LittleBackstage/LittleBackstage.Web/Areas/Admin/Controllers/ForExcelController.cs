using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Core.Services;
using LittleBackstage.Infrastructure.Services;
using LittleBackstage.Web.Helpers;
using System.IO;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using LittleBackstage.Core.Models;

namespace LittleBackstage.Web.Areas.Admin.Controllers
{
    public class ForExcelController : Controller
    {
        private readonly IHelperServices _helperServices;
        private readonly IForExcelService _forExcelService;
        private readonly ILogService _log;


        public ForExcelController(IHelperServices helperServices, IForExcelService forExcelService, ILogService log)
        {
            _helperServices = helperServices;
            _forExcelService = forExcelService;
            _log = log;
        }


        /// <summary>
        /// 高级检索
        /// </summary>
        /// <param name="state">status=1 进行全文匹配，如果status=2进行正题名匹配，如果status=3进行演奏方式匹配，status=4进行民族匹配</param>
        /// <param name="key">关键字</param>
        /// <param name="yzfs"></param>
        /// <param name="mz"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SeniorSearch(int state, string key, string yzfs, string mz, int pageSize, int pageIndex)
        {
            var totalCount = 0;
            if (string.IsNullOrEmpty(key) && string.IsNullOrEmpty(yzfs) && string.IsNullOrEmpty(mz))
            {
                var all = _forExcelService.List(pageIndex, pageSize, ref totalCount);
                var jsonData = new JsonData();
                jsonData.totalCount = totalCount;
                jsonData.list = all.ToList();
                return Json(jsonData, JsonRequestBehavior.DenyGet);
            }

            var result = _forExcelService.SeniorSearch(state, key, yzfs, mz, pageIndex, pageSize, ref totalCount);
            var jsonData2 = new JsonData();
            jsonData2.totalCount = totalCount;
            jsonData2.list = result.ToList();
            return Json(jsonData2, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Find(int id)
        {
            var result = _forExcelService.Get(id);
            return Json(result, JsonRequestBehavior.DenyGet);
        }


        /// <summary>
        /// 导入 的post
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ImportFile()
        {
            string path = HttpContext.Server.MapPath("~/Uploads/");
            //var file = SaveImg(path, Request.Files["file"]);
            //StreamReader sr = new StreamReader(path + file, System.Text.Encoding.Default);
            //string data = sr.ReadToEnd();
            //sr.Close();
            #region 解析 文件 导入 数据 _worldHeritageService

            //解析文件 读取文件 导出datatable
            DirectoryInfo di = new DirectoryInfo(path + "\\fyexcel");
            DirectoryInfo[] dir = di.GetDirectories();//获取子文件夹列表
            var wh = new List<ForExcel>();

            foreach (FileInfo file in di.GetFiles())
            {
                //Console.WriteLine(file.Name);
                //ExcelHelper eh = new ExcelHelper("C:\\Users\\Administrator\\Desktop\\fyexcel\\" + file.Name);
                DataTable dt = new DataTable();
                DataTable dtimg = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataTable dt4 = new DataTable();
                DataTable dt5 = new DataTable();
                DataTable dt6 = new DataTable();
                DataTable dt7 = new DataTable();
                DataTable dt8 = new DataTable();
                DataTable dt9 = new DataTable();
                dt = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 0, 4, 49);
                dtimg = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 1, 3, 15);
                dt2 = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 2, 3, 5);
                dt3 = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 3, 3, 5);
                dt4 = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 4, 3, 5);
                dt5 = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 5, 5, 18);
                dt6 = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 6, 3, 4);
                dt7 = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 7, 3, 4);
                dt8 = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 8, 3, 13);
                dt9 = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 9, 3, 10);
                //Console.WriteLine(dt.Rows.count);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var wh1 = new ForExcel();
                    var rgcode = dt.Rows[i][1].ToString();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        //if (j == 0)
                        //    wh1.InventoryId = dt.Rows[i][j].ToString();
                        if (j == 1)
                        {
                            //wh1.ArtificialId = dt.Rows[i][j].ToString();
                            //wh1.FileName = "~/Uploads/importVideo/" + dt.Rows[i][j].ToString() + ".mp4";
                            wh1.RenGongBianMa = dt.Rows[i][j].ToString();
                        }
                        if (j == 2)//正题名
                            wh1.TiMing_ZhengTiMing = dt.Rows[i][j].ToString();
                        if (j == 3)
                            wh1.TiMing_QiTaTiMing = dt.Rows[i][j].ToString();
                        if (j == 4)
                            wh1.LeiBie_YanZouFangShi = dt.Rows[i][j].ToString();
                        if (j == 5)
                            wh1.LeiBie_HuoSa = dt.Rows[i][j].ToString();
                        if (j == 6)
                            wh1.YueZhong = dt.Rows[i][j].ToString();
                        if (j == 7)
                            wh1.MinZuShuXing = dt.Rows[i][j].ToString();
                        if (j == 8)
                            wh1.LaiYuan_SuoYouZhe = dt.Rows[i][j].ToString();
                        if (j == 9)
                            wh1.LaiYuan_ZhengJiShiJian = dt.Rows[i][j].ToString();
                        if (j == 10)
                            wh1.LaiYuan_ZhengJiDiDian = dt.Rows[i][j].ToString();
                        if (j == 11)
                            wh1.WenHuaBeiJing_LiShiYuanLiu = dt.Rows[i][j].ToString();
                        if (j == 12)
                            wh1.WenHuaBeiJing_LiuBuDiYu = dt.Rows[i][j].ToString();
                        if (j == 13)
                            wh1.WenHuaBeiJing_ChuanCheng_ZhiZuo = dt.Rows[i][j].ToString();
                        if (j == 14)
                            wh1.WenHuaBeiJing_ChuanCheng_YanZou = dt.Rows[i][j].ToString();
                        if (j == 15)
                            wh1.WenHuaBeiJing_ShiYuongChangSuo = dt.Rows[i][j].ToString();
                        if (j == 16)
                            wh1.WenHuaBeiJing_SheHuiGongNeng = dt.Rows[i][j].ToString();
                        if (j == 17)
                            wh1.YueQiXingZhi_JieGouShiYiTu = dt.Rows[i][j].ToString();
                        if (j == 19)
                            wh1.YueQiXingZhi_FaYinTi = dt.Rows[i][j].ToString();
                        if (j == 20)
                            wh1.YueQiXingZhi_GongMingTi = dt.Rows[i][j].ToString();
                        if (j == 21)
                            wh1.YueQiXingZhi_QiTa = dt.Rows[i][j].ToString();
                        if (j == 22)
                            wh1.YueQiGongYi_JiBenGongYi = dt.Rows[i][j].ToString();
                        if (j == 23)
                            wh1.YueQiGongYi_TeShuGongYi_QiYi = dt.Rows[i][j].ToString();
                        if (j == 24)
                            wh1.YueQiGongYi_TeShuGongYi_YanSe = dt.Rows[i][j].ToString();
                        if (j == 25)
                            wh1.YueQiGongYi_TeShuGongYi_QiTa = dt.Rows[i][j].ToString();
                        if (j == 26)
                            wh1.YueQiGongYi_ZhiZuoShiJian = dt.Rows[i][j].ToString();
                        if (j == 27)
                            wh1.YueQiGongYi_ZhiZuoDiDian = dt.Rows[i][j].ToString();
                        if (j == 28)
                            wh1.YueQiGongYi_ZhiZuoZhe = dt.Rows[i][j].ToString();
                        if (j == 29)
                            wh1.YueQiGongYi_ZhiZuoLiuCheng = dt.Rows[i][j].ToString();
                        if (j == 30)
                            wh1.YueQiGongYi_GaiLiangQingKuang = dt.Rows[i][j].ToString();
                        if (j == 31)
                            wh1.YueQiYiXiang_FaShengFangShi = dt.Rows[i][j].ToString();
                        if (j == 32)
                            wh1.YueQiGongYi_YiLu_DiaoGao = dt.Rows[i][j].ToString();
                        if (j == 33)
                            wh1.YueQiGongYi_YiLu_TongYin = dt.Rows[i][j].ToString();
                        if (j == 34)
                            wh1.YueQiGongYi_YiLu_DingXuan = dt.Rows[i][j].ToString();
                        if (j == 35)
                            wh1.YueQiGongYi_YinLie = dt.Rows[i][j].ToString();
                        if (j == 36)
                            wh1.YueQiGongYi_TiaoYinFa = dt.Rows[i][j].ToString();
                        if (j == 37)
                            wh1.YanZouJiFa_YanZouJiFa = dt.Rows[i][j].ToString();
                        if (j == 38)
                            wh1.YanZouJiFa_ShiFanYueQu = dt.Rows[i][j].ToString();
                        if (j == 39)
                            wh1.YanZouJiFa_YanZouZheJiBenXinXi = dt.Rows[i][j].ToString();
                        if (j == 40)
                            wh1.CaiLuXinXi_CaiLuZhe = dt.Rows[i][j].ToString();
                        if (j == 41)
                            wh1.CaiLuXinXi_CaiLuShiJian = dt.Rows[i][j].ToString();
                        if (j == 42)
                            wh1.CaiLuXinXi_DiDian = dt.Rows[i][j].ToString();
                        if (j == 43)
                            wh1.CaiLuXinXi_CaiLuHuanJing = dt.Rows[i][j].ToString();
                        if (j == 44)
                            wh1.CaiLuXinXi_ZhuLuZhe = dt.Rows[i][j].ToString();
                        if (j == 45)
                            wh1.CaiLuXinXi_CaiLuDaoYan = dt.Rows[i][j].ToString();
                        if (j == 46)
                            wh1.CaiLuXinXi_BanQuan = dt.Rows[i][j].ToString();
                        if (j == 47)
                            wh1.YueQiJianJie = dt.Rows[i][j].ToString();
                        if (j == 48)
                            wh1.ShengXueCeLiangYuPinPuFenXiBaoGao = dt.Rows[i][j].ToString();
                        if (j == 49)
                            wh1.CeLiangXinXiJiLuBiao = dt.Rows[i][j].ToString();
                    }
                    //附表发音体
                    for (int a = 0; a < dt2.Rows.Count; a++)
                    {
                        if (dt2.Rows[a][0].ToString() == rgcode)
                        {
                            if (dt2.Rows[a][1].ToString() != "")
                                wh1.Kaleida_MingCheng = string.IsNullOrEmpty(wh1.Kaleida_MingCheng) ? dt2.Rows[a][1].ToString() : wh1.Kaleida_MingCheng + ',' + dt2.Rows[a][1].ToString();
                            if (dt2.Rows[a][2].ToString() != "")
                                wh1.Kaleida_JieGouChiCun = string.IsNullOrEmpty(wh1.Kaleida_JieGouChiCun) ? dt2.Rows[a][2].ToString() : wh1.Kaleida_JieGouChiCun + ',' + dt2.Rows[a][2].ToString();
                            if (dt2.Rows[a][3].ToString() != "")
                                wh1.Kaleida_CaiZhi = string.IsNullOrEmpty(wh1.Kaleida_CaiZhi) ? dt2.Rows[a][3].ToString() : wh1.Kaleida_CaiZhi + ',' + dt2.Rows[a][3].ToString();
                            if (dt2.Rows[a][4].ToString() != "")
                                wh1.Kaleida_ZhongLiang = string.IsNullOrEmpty(wh1.Kaleida_ZhongLiang) ? dt2.Rows[a][4].ToString() : wh1.Kaleida_ZhongLiang + ',' + dt2.Rows[a][4].ToString();
                        }
                    }
                    //附表共鸣体
                    for (int a = 0; a < dt3.Rows.Count; a++)
                    {
                        if (dt3.Rows[a][0].ToString() == rgcode)
                        {
                            if (dt3.Rows[a][0].ToString() == rgcode)
                            {
                                if (dt3.Rows[a][1].ToString() != "")
                                    wh1.Resonator_MingCheng = string.IsNullOrEmpty(wh1.Resonator_MingCheng) ? dt3.Rows[a][1].ToString() : wh1.Resonator_MingCheng + ',' + dt3.Rows[a][1].ToString();
                                if (dt3.Rows[a][2].ToString() != "")
                                    wh1.Resonator_JieGouChiCun = string.IsNullOrEmpty(wh1.Resonator_JieGouChiCun) ? dt3.Rows[a][2].ToString() : wh1.Resonator_JieGouChiCun + ',' + dt3.Rows[a][2].ToString();
                                if (dt3.Rows[a][3].ToString() != "")
                                    wh1.Resonator_CaiZhi = string.IsNullOrEmpty(wh1.Resonator_CaiZhi) ? dt3.Rows[a][3].ToString() : wh1.Resonator_CaiZhi + ',' + dt3.Rows[a][3].ToString();
                                if (dt3.Rows[a][4].ToString() != "")
                                    wh1.Resonator_ZhongLiang = string.IsNullOrEmpty(wh1.Resonator_ZhongLiang) ? dt3.Rows[a][4].ToString() : wh1.Resonator_ZhongLiang + ',' + dt3.Rows[a][4].ToString();
                            }
                        }
                    }
                    //附表其他
                    for (int a = 0; a < dt4.Rows.Count; a++)
                    {
                        if (dt4.Rows[a][0].ToString() == rgcode)
                        {
                            if (dt4.Rows[a][1].ToString() != "")
                                wh1.Other_MingCheng = string.IsNullOrEmpty(wh1.Other_MingCheng) ? dt4.Rows[a][1].ToString() : wh1.Other_MingCheng + ',' + dt4.Rows[a][1].ToString();
                            if (dt4.Rows[a][2].ToString() != "")
                                wh1.Other_JieGouChiCun = string.IsNullOrEmpty(wh1.Other_JieGouChiCun) ? dt4.Rows[a][2].ToString() : wh1.Other_JieGouChiCun + ',' + dt4.Rows[a][2].ToString();
                            if (dt4.Rows[a][3].ToString() != "")
                                wh1.Other_CaiZhi = string.IsNullOrEmpty(wh1.Other_CaiZhi) ? dt4.Rows[a][3].ToString() : wh1.Other_CaiZhi + ',' + dt4.Rows[a][3].ToString();
                            if (dt4.Rows[a][4].ToString() != "")
                                wh1.Other_ZhongLiang = string.IsNullOrEmpty(wh1.Other_ZhongLiang) ? dt4.Rows[a][4].ToString() : wh1.Other_ZhongLiang + ',' + dt4.Rows[a][4].ToString();
                        }
                    }
                    //附表音列
                    for (int a = 0; a < dt5.Rows.Count; a++)
                    {
                        if (dt5.Rows[a][0].ToString() == rgcode)
                        {
                            wh1.YikHz = dt5.Rows[a][1].ToString();
                            wh1.KongXianYin_ZiRanShuaiJian_P_First = dt5.Rows[a][2].ToString();
                            wh1.KongXianYin_ZiRanShuaiJian_P_Second = dt5.Rows[a][3].ToString();
                            wh1.KongXianYin_ZiRanShuaiJian_F_First = dt5.Rows[a][4].ToString();
                            wh1.KongXianYin_ZiRanShuaiJian_F_Second = dt5.Rows[a][5].ToString();
                            wh1.KongXianYin_ZiRanShuaiJian_MF_First = dt5.Rows[a][6].ToString();
                            wh1.KongXianYin_ZiRanShuaiJian_MF_Second = dt5.Rows[a][7].ToString();
                            wh1.YinJie_ZiRanShuaiJian_MF_First = dt5.Rows[a][8].ToString();
                            wh1.YinJie_ZiRanShuaiJian_MF_Second = dt5.Rows[a][9].ToString();
                            wh1.YinJie_ManSu_P_First = dt5.Rows[a][10].ToString();
                            wh1.YinJie_ManSu_P_Second = dt5.Rows[a][11].ToString();
                            wh1.YinJie_ManSu_F_First = dt5.Rows[a][12].ToString();
                            wh1.YinJie_ManSu_F_Second = dt5.Rows[a][13].ToString();
                            wh1.YinJie_ManSu_MF_First = dt5.Rows[a][14].ToString();
                            wh1.YinJie_ManSu_MF_Second = dt5.Rows[a][15].ToString();
                            wh1.YinJie_ZhongSu_F_First = dt5.Rows[a][16].ToString();
                            wh1.YinJie_ZhongSu_F_Second = dt5.Rows[a][17].ToString();
                        }
                    }
                    //附表演奏技法
                    for (int a = 0; a < dt6.Rows.Count; a++)
                    {
                        if (dt6.Rows[a][0].ToString() == rgcode)
                        {
                            if (dt6.Rows[a][1].ToString() != "")
                                wh1.YanZhouJiFa_MingChen = string.IsNullOrEmpty(wh1.YanZhouJiFa_MingChen) ? dt6.Rows[a][1].ToString() : wh1.YanZhouJiFa_MingChen + ',' + dt6.Rows[a][1].ToString();
                            if (dt6.Rows[a][2].ToString() != "")
                                wh1.YanZhouJiFa_First = string.IsNullOrEmpty(wh1.YanZhouJiFa_First) ? dt6.Rows[a][2].ToString() : wh1.YanZhouJiFa_First + ',' + dt6.Rows[a][2].ToString();
                            if (dt6.Rows[a][3].ToString() != "")
                                wh1.YanZhouJiFa_Second = string.IsNullOrEmpty(wh1.YanZhouJiFa_Second) ? dt6.Rows[a][3].ToString() : wh1.YanZhouJiFa_Second + ',' + dt6.Rows[a][3].ToString();
                        }
                    }
                    //附表示范乐曲
                    for (int a = 0; a < dt7.Rows.Count; a++)
                    {
                        if (dt7.Rows[a][0].ToString() == rgcode)
                        {
                            if (dt7.Rows[a][1].ToString() != "")
                                wh1.ShiFanYuQu_QuMuMing = string.IsNullOrEmpty(wh1.ShiFanYuQu_QuMuMing) ? dt7.Rows[a][1].ToString() : wh1.ShiFanYuQu_QuMuMing + ',' + dt7.Rows[a][1].ToString();
                            if (dt7.Rows[a][2].ToString() != "")
                                wh1.ShiFanYuQu_YinPin = string.IsNullOrEmpty(wh1.ShiFanYuQu_YinPin) ? dt7.Rows[a][2].ToString() : wh1.ShiFanYuQu_YinPin + ',' + dt7.Rows[a][2].ToString();
                            if (dt7.Rows[a][3].ToString() != "")
                                wh1.ShiFanYuQu_ShiPin = string.IsNullOrEmpty(wh1.ShiFanYuQu_ShiPin) ? dt7.Rows[a][3].ToString() : wh1.ShiFanYuQu_ShiPin + ',' + dt7.Rows[a][3].ToString();
                        }
                    }
                    //附表演奏者基本信息
                    for (int a = 0; a < dt8.Rows.Count; a++)
                    {
                        if (dt8.Rows[a][0].ToString() == rgcode)
                        {
                            wh1.Other = dt8.Rows[a][1].ToString();
                            wh1.JiBenXinXi_XingBie = dt8.Rows[a][2].ToString();
                            wh1.JiBenXinXi_ChuShengNianYue = dt8.Rows[a][3].ToString();
                            wh1.JiBenXinXi_JiGuan = dt8.Rows[a][4].ToString();
                            wh1.JiBenXinXi_XueLi = dt8.Rows[a][5].ToString();
                            wh1.JiBenXinXi_ZhuanYe = dt8.Rows[a][6].ToString();
                            wh1.JiBenXinXi_ZhiCheng = dt8.Rows[a][7].ToString();
                            wh1.JiBenXinXi_XueSuoYanZouYueQiShiJian = dt8.Rows[a][8].ToString();
                            wh1.JiBenXinXi_LianXiFangShi = dt8.Rows[a][9].ToString();
                            wh1.JiBenXinXi_GongZuoDanWei = dt8.Rows[a][10].ToString();
                            wh1.JiBenXinXi_TingLiZhuangKuang = dt8.Rows[a][11].ToString();
                            wh1.JiBenXinXi_ShiChengGuanXi = dt8.Rows[a][12].ToString();
                            wh1.JiBenXinXi_ChuanLue = dt8.Rows[a][13].ToString();
                        }
                    }
                    //附表采录环境
                    for (int a = 0; a < dt9.Rows.Count; a++)
                    {
                        if (dt9.Rows[a][0].ToString() == rgcode)
                        {
                            wh1.LuYinHuanJin_WenDu = dt9.Rows[a][1].ToString();
                            wh1.LuYinHuanJin_ShiDu = dt9.Rows[a][2].ToString();
                            wh1.LuYinHuanJin_DaQiYa = dt9.Rows[a][3].ToString();
                            wh1.LuYinHuanJin_BenDiZhaoSheng = dt9.Rows[a][4].ToString();
                            wh1.LuYinHuanJin_LuYinPengChiCun = dt9.Rows[a][5].ToString();
                            wh1.LuYinHuanJin_HunXiangShiJian = dt9.Rows[a][6].ToString();
                            wh1.LuYinHuanJin_LuYinRuanJian = dt9.Rows[a][7].ToString();
                            wh1.LuYinHuanJin_LuYinSheBei = dt9.Rows[a][8].ToString();
                            wh1.LuYinHuanJin_ShiYinTu = dt9.Rows[a][9].ToString();
                        }
                    }

                    //不同角度图片
                    for (int a = 0; a < dtimg.Rows.Count; a++)
                    {
                        if (dtimg.Rows[a][0].ToString() == rgcode)
                        {
                            if (dtimg.Rows[a][1].ToString() != "")
                                wh1.Img = string.IsNullOrEmpty(wh1.Img) ? dtimg.Rows[a][1].ToString() : wh1.Img + ',' + dtimg.Rows[a][1].ToString();
                            if (dtimg.Rows[a][2].ToString() != "")
                                wh1.Img = string.IsNullOrEmpty(wh1.Img) ? dtimg.Rows[a][2].ToString() : wh1.Img + ',' + dtimg.Rows[a][2].ToString();
                            if (dtimg.Rows[a][3].ToString() != "")
                                wh1.Img = string.IsNullOrEmpty(wh1.Img) ? dtimg.Rows[a][3].ToString() : wh1.Img + ',' + dtimg.Rows[a][3].ToString();
                            if (dtimg.Rows[a][4].ToString() != "")
                                wh1.Img = string.IsNullOrEmpty(wh1.Img) ? dtimg.Rows[a][4].ToString() : wh1.Img + ',' + dtimg.Rows[a][4].ToString();
                            if (dtimg.Rows[a][5].ToString() != "")
                                wh1.Img = string.IsNullOrEmpty(wh1.Img) ? dtimg.Rows[a][5].ToString() : wh1.Img + ',' + dtimg.Rows[a][5].ToString();
                            if (dtimg.Rows[a][6].ToString() != "")
                                wh1.Img = string.IsNullOrEmpty(wh1.Img) ? dtimg.Rows[a][6].ToString() : wh1.Img + ',' + dtimg.Rows[a][6].ToString();
                            if (dtimg.Rows[a][7].ToString() != "")
                                wh1.Img = string.IsNullOrEmpty(wh1.Img) ? dtimg.Rows[a][7].ToString() : wh1.Img + ',' + dtimg.Rows[a][7].ToString();
                            if (dtimg.Rows[a][8].ToString() != "")
                                wh1.Img = string.IsNullOrEmpty(wh1.Img) ? dtimg.Rows[a][8].ToString() : wh1.Img + ',' + dtimg.Rows[a][8].ToString();
                            if (dtimg.Rows[a][9].ToString() != "")
                                wh1.Img = string.IsNullOrEmpty(wh1.Img) ? dtimg.Rows[a][9].ToString() : wh1.Img + ',' + dtimg.Rows[a][9].ToString();
                            if (dtimg.Rows[a][10].ToString() != "")
                                wh1.Img = string.IsNullOrEmpty(wh1.Img) ? dtimg.Rows[a][10].ToString() : wh1.Img + ',' + dtimg.Rows[a][10].ToString();
                            if (dtimg.Rows[a][11].ToString() != "")
                                wh1.Img = string.IsNullOrEmpty(wh1.Img) ? dtimg.Rows[a][11].ToString() : wh1.Img + ',' + dtimg.Rows[a][11].ToString();
                            if (dtimg.Rows[a][12].ToString() != "")
                                wh1.Img = string.IsNullOrEmpty(wh1.Img) ? dtimg.Rows[a][12].ToString() : wh1.Img + ',' + dtimg.Rows[a][12].ToString();
                            if (dtimg.Rows[a][13].ToString() != "")
                                wh1.Img = string.IsNullOrEmpty(wh1.Img) ? dtimg.Rows[a][13].ToString() : wh1.Img + ',' + dtimg.Rows[a][13].ToString();
                            if (dtimg.Rows[a][14].ToString() != "")
                                wh1.Img = string.IsNullOrEmpty(wh1.Img) ? dtimg.Rows[a][14].ToString() : wh1.Img + ',' + dtimg.Rows[a][14].ToString();
                        }
                    }

                    wh1.CreatedUtc = DateTime.Now;
                    wh.Add(wh1);
                }
            }

            _forExcelService.Import(wh);//一次性 录入数据 没测试 导入的时候你测试一下

            #endregion
            return RedirectToAction("List");
        }
        private IWorkbook workbook = null;
        private FileStream fs = null;
        //从excel导出到datatable
        public DataTable ExcelToDataTable(string fileName, int tableIndex, int startRowCount, int totalRowCount)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                //判断excel版本
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003
                    workbook = new HSSFWorkbook(fs);

                //获取sheet 通过参数指定哪个表
                sheet = workbook.GetSheetAt(tableIndex);

                if (sheet != null)
                {
                    int firstrownum = startRowCount;
                    IRow firstRow = sheet.GetRow(firstrownum);
                    int cellCount = totalRowCount;//firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    //添加列头
                    for (int i = 0; i < totalRowCount + 1; i++)
                    {
                        string cellValue = i.ToString();
                        DataColumn column = new DataColumn(cellValue);
                        data.Columns.Add(column);
                    }
                    startRow = startRowCount;

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = 0; j <= cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                            else
                                dataRow[j] = "";
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

    }
}