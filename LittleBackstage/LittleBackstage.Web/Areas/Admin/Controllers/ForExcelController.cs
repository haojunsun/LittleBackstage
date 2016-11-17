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
        private readonly ILiteratureExcelServices _forExcelService;
        private readonly ILogService _log;


        public ForExcelController(IHelperServices helperServices, ILiteratureExcelServices forExcelService, ILogService log)
        {
            _helperServices = helperServices;
            _forExcelService = forExcelService;
            _log = log;
        }


        /// <summary>
        /// 高级检索
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fl"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SeniorSearch(string key, string fl, int pageSize, int pageIndex)
        {
            var totalCount = 0;
            if (string.IsNullOrEmpty(key) && string.IsNullOrEmpty(fl))
            {
                var all = _forExcelService.List(pageIndex, pageSize, ref totalCount);
                var jsonData = new JsonData();
                jsonData.totalCount = totalCount;
                jsonData.list = all.ToList();
                return Json(jsonData, JsonRequestBehavior.DenyGet);
            }

            var result = _forExcelService.SeniorSearch(key, fl, pageIndex, pageSize, ref totalCount);
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
            var wh = new List<LiteratureExcel>();

            foreach (FileInfo file in di.GetFiles())
            {
                //Console.WriteLine(file.Name);
                //ExcelHelper eh = new ExcelHelper("C:\\Users\\Administrator\\Desktop\\fyexcel\\" + file.Name);
                DataTable dt = new DataTable();

                dt = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 0, 4, 60);

                //Console.WriteLine(dt.Rows.count);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var wh1 = new LiteratureExcel();
                    var rgcode = dt.Rows[i][1].ToString();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        //if (j == 0)
                        //    wh1.InventoryId = dt.Rows[i][j].ToString();
                        if (j == 1)
                            wh1.ArtificialId = dt.Rows[i][j].ToString();
                        if (j == 2)//文件名
                            wh1.TitleProper = dt.Rows[i][j].ToString();
                        if (j == 3)
                            wh1.XiangMuMingCheng = dt.Rows[i][j].ToString();
                        if (j == 4)
                            wh1.FirstLevel = dt.Rows[i][j].ToString();
                        if (j == 5)//二级分类
                            wh1.SecondLevel = dt.Rows[i][j].ToString();
                        if (j == 6)
                            wh1.XiangMuBianMa = dt.Rows[i][j].ToString();
                        if (j == 7)
                            wh1.ZiXiangXuHao = dt.Rows[i][j].ToString();
                        if (j == 8)//批次
                            wh1.PiCi = dt.Rows[i][j].ToString();
                        if (j == 9)
                            wh1.Annotation = dt.Rows[i][j].ToString();
                        if (j == 10)
                            wh1.XiangMuShenQingShiJian = dt.Rows[i][j].ToString();
                        if (j == 11)
                            wh1.MinZu = dt.Rows[i][j].ToString();
                        if (j == 12)//直系
                            wh1.ZhiXi = dt.Rows[i][j].ToString();
                        if (j == 13)
                            wh1.XiangMuJianJieBiaoZhunBan = dt.Rows[i][j].ToString();
                        if (j == 14)
                            wh1.XiangMuJianJie = dt.Rows[i][j].ToString();
                        if (j == 15)
                            wh1.SuoZaiQuYuJiQiDiLiHuanJing = dt.Rows[i][j].ToString();
                        if (j == 16)//分布区域
                            wh1.FenBuQuYu = dt.Rows[i][j].ToString();
                        if (j == 17)
                            wh1.LiShiYuanYuan = dt.Rows[i][j].ToString();
                        if (j == 18)
                            wh1.JiBenNeiRong = dt.Rows[i][j].ToString();
                        if (j == 19)
                            wh1.XiangGuanZhiPinJiQiZuoPin = dt.Rows[i][j].ToString();
                        if (j == 20)//传承谱系
                            wh1.ChuanChengPuXi = dt.Rows[i][j].ToString();
                        if (j == 21)
                            wh1.DaiBiaoXingChuanChengRen = dt.Rows[i][j].ToString();
                        if (j == 22)
                            wh1.ZhuYaoTeZheng = dt.Rows[i][j].ToString();
                        if (j == 23)
                            wh1.ZhongYaoJiaZhi = dt.Rows[i][j].ToString();
                        if (j == 24)
                            wh1.CunXuZhuangKuang = dt.Rows[i][j].ToString();
                        //if (j == 25)//主要传承人
                        //    wh1.ZhuYaoChuanChengRen = dt.Rows[i][j].ToString();
                        //if (j == 26)//项目名称 重复
                        //wh1.Type = dt.Rows[i][j].ToString();
                        if (j == 27)
                            wh1.Type = wh1.Type + "," + dt.Rows[i][j].ToString();
                        if (j == 28)
                            wh1.Type = wh1.Type + "," + dt.Rows[i][j].ToString();
                        if (j == 29)
                            wh1.Type = wh1.Type + "," + dt.Rows[i][j].ToString();
                        if (j == 30)
                            wh1.Type = wh1.Type + "," + dt.Rows[i][j].ToString();
                        if (j == 31)
                            wh1.Type = wh1.Type + "," + dt.Rows[i][j].ToString();
                        if (j == 32)//主要传承人记录视频
                            wh1.ZhuYaoChuanChengRen = dt.Rows[i][j].ToString();
                        if (j == 33)
                            wh1.CoverageSpatial_Province = dt.Rows[i][j].ToString();
                        if (j == 34)
                            wh1.CoverageSpatial_City = dt.Rows[i][j].ToString();
                        if (j == 35)
                            wh1.CoverageSpatial_County = dt.Rows[i][j].ToString();
                        if (j == 36)
                            wh1.CoverageSpatial_ShenBaoDiQuHuoDanWei = dt.Rows[i][j].ToString();
                        if (j == 37)//保护单位
                            wh1.CoverageSpatial_BaoHuDanWei = dt.Rows[i][j].ToString();
                        if (j == 38)
                            wh1.CoverageSpatial_ShengTaiQuXiangMu = dt.Rows[i][j].ToString();
                        if (j == 39)
                            wh1.CoverageSpatial_ShengChanXingBaoHuShiFanJiDi = dt.Rows[i][j].ToString();
                        if (j == 40)
                            wh1.Source_CreatorOfReferences_Name = dt.Rows[i][j].ToString();
                        if (j == 41)
                            wh1.Source_CreatorOfReferences_Role = dt.Rows[i][j].ToString();
                        if (j == 42)
                            wh1.Source_ProviderOfReferences = dt.Rows[i][j].ToString();
                        if (j == 43)
                            wh1.Source_RepositoryName = dt.Rows[i][j].ToString();
                        if (j == 44)
                            wh1.DigitalObjectInformation_DigitalObjectFormat = dt.Rows[i][j].ToString();
                        if (j == 45)
                            wh1.DigitalObjectInformation_Size = dt.Rows[i][j].ToString();
                        if (j == 46)
                            wh1.DigitalObjectInformation_Duration = dt.Rows[i][j].ToString();
                        if (j == 47)
                            wh1.DigitalObjectInformation_DigitalSpecification = dt.Rows[i][j].ToString();
                        if (j == 48)
                            wh1.DigitalObjectInformation_AudioVideo = dt.Rows[i][j].ToString();
                        if (j == 49)
                            wh1.DigitalObjectInformation_AudioSamplingFrequency = dt.Rows[i][j].ToString();
                        if (j == 50)
                            wh1.DigitalObjectInformation_NumberOfChannels = dt.Rows[i][j].ToString();
                        if (j == 51)
                            wh1.DigitalObjectInformation_FilePath = dt.Rows[i][j].ToString();
                        if (j == 52)
                            wh1.DigitalObjectInformation_DisplayType = dt.Rows[i][j].ToString();
                        if (j == 53)
                            wh1.RecordingInformation_Recorder = dt.Rows[i][j].ToString();
                        if (j == 54)
                            wh1.RecordingInformation_RecordingTime = dt.Rows[i][j].ToString();
                        if (j == 55)
                            wh1.RecordingInformation_MetadataAuditor = dt.Rows[i][j].ToString();
                        if (j == 56)
                            wh1.RecordingInformation_MetadataAuditoTime = dt.Rows[i][j].ToString();
                        if (j == 57)
                            wh1.RecordingInformation_MetadataManagement = dt.Rows[i][j].ToString();
                        if (j == 58)
                            wh1.RecordingInformation_Note = dt.Rows[i][j].ToString();
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