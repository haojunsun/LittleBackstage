using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Core.Basis;
using LittleBackstage.Core.Models;
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

        private readonly IManagerService _managerService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly ILogService _logService;
        private readonly ICategoryFieldService _categoryFieldService;
        private readonly ISystemLogService _systemLogService;

        public ForExcelController(IHelperServices helperServices, ILiteratureExcelServices forExcelService, ILogService log, IManagerService managerService,
            IRoleService roleService,
            IUserService userService,
            ICategoryService categoryService,
            ILogService logService,
            ICategoryFieldService categoryFieldService,
            ISystemLogService systemLogService)
        {
            _helperServices = helperServices;
            _forExcelService = forExcelService;
            _log = log;

            _managerService = managerService;
            _roleService = roleService;
            _userService = userService;
            _categoryService = categoryService;
            _logService = logService;
            _categoryFieldService = categoryFieldService;
            _systemLogService = systemLogService;
        }


        /// <summary>
        /// 高级检索
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fl"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult SeniorSearch(int pageSize, int pageIndex, int id = 0)
        {
            var totalCount = 0;
            var c = _categoryService.List().First(x => x.IsCreateTable == 1 && x.CategoryId == id);
            if (c != null)
            {
                var table = new DataTable();

                var sql = @"select * from " + c.DataTableName;
                table = SqlHelper.QueryDataTable(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null);
                totalCount = table.Rows.Count;//数据总数
                var list = GetPagedTable(table, pageIndex, pageSize);
                var json = new
                {
                    list,
                    totalCount
                };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)//PageIndex表示第几页，PageSize表示每页的记录数
        {
            if (PageIndex == 0)
                return dt;//0页代表每页数据，直接返回

            DataTable newdt = dt.Copy();
            newdt.Clear();//copy dt的框架

            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;//源数据记录数小于等于要显示的记录，直接返回dt
            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
        }

        //public ActionResult SeniorSearch(string key, string fl, int pageSize, int pageIndex)
        //{
        //    var totalCount = 0;
        //    if (string.IsNullOrEmpty(key) && string.IsNullOrEmpty(fl))
        //    {
        //        var all = _forExcelService.List(pageIndex, pageSize, ref totalCount);
        //        var jsonData = new JsonData();
        //        jsonData.totalCount = totalCount;
        //        jsonData.list = all.ToList();
        //        return Json(jsonData, JsonRequestBehavior.DenyGet);
        //    }

        //    var result = _forExcelService.SeniorSearch(key, fl, pageIndex, pageSize, ref totalCount);
        //  var jsonData2 = new JsonData();
        //    jsonData2.totalCount = totalCount;
        //    jsonData2.list = result.ToList();
        //    return Json(jsonData2, JsonRequestBehavior.DenyGet);
        //}



        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="cid">模板id</param>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult Find(int cid,int id)
        {
            var c = _categoryService.List().First(x => x.IsCreateTable == 1 && x.CategoryId == cid);
            if (c != null)
            {
                var sql = @"select * from " + c.DataTableName + " where [" + c.DataTableName + "_Id]=" + id;
                var table = SqlHelper.QueryDataTable(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null);
                var json = new
                {
                    table
                };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
            //var result = _forExcelService.Get(id);
            //return Json(result, JsonRequestBehavior.DenyGet);
        }


        /// <summary>
        /// 导入 的post
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ImportFile(int categoryId)
        {
            string path = HttpContext.Server.MapPath("~/Uploads/");
            //var file = SaveImg(path, Request.Files["file"]);
            //StreamReader sr = new StreamReader(path + file, System.Text.Encoding.Default);
            //string data = sr.ReadToEnd();
            //sr.Close();

            var title = "";
            var category = _categoryService.Get(categoryId);
            if (category != null && category.IsCreateTable == 1 && category.CategoryFields.Any())
            {
                //解析文件 读取文件 导出datatable
                DirectoryInfo di = new DirectoryInfo(path + "\\fyexcel");
                DirectoryInfo[] dir = di.GetDirectories();//获取子文件夹列表
                var wh = new List<LiteratureExcel>();
                foreach (FileInfo file in di.GetFiles())
                {
                    //ExcelHelper eh = new ExcelHelper("C:\\Users\\Administrator\\Desktop\\fyexcel\\" + file.Name);
                    DataTable dt = new DataTable();

                    dt = ExcelToDataTable(path + "\\fyexcel\\" + file.Name, 0, 4, 60);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var wh1 = new LiteratureExcel();
                        var rgcode = dt.Rows[i][1].ToString();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            //if (j == 0)
                            //    wh1.InventoryId = dt.Rows[i][j].ToString().Replace("'","\"");
                            if (j == 1)//人工编码
                                wh1.ArtificialId = dt.Rows[i][j].ToString();
                            if (j == 2)//文件名
                                wh1.TitleProper = dt.Rows[i][j].ToString().Replace("'", "\"").Replace("'", "\"");
                            if (j == 3)//项目名称
                                wh1.XiangMuMingCheng = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 4)//一级分类
                                wh1.FirstLevel = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 5)//二级分类
                                wh1.SecondLevel = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 6)//项目编码
                                wh1.XiangMuBianMa = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 7)//子项序号
                                wh1.ZiXiangXuHao = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 8)//批次
                                wh1.PiCi = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 9)//注释
                                wh1.Annotation = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 10)//项目申请时间
                                wh1.XiangMuShenQingShiJian = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 11)//民族
                                wh1.MinZu = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 12)//直系
                                wh1.ZhiXi = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 13)//项目简介标准版
                                wh1.XiangMuJianJieBiaoZhunBan = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 14)//项目简介
                                wh1.XiangMuJianJie = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 15)//所在区域及其地理环境
                                wh1.SuoZaiQuYuJiQiDiLiHuanJing = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 16)//分布区域
                                wh1.FenBuQuYu = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 17)//历史渊源
                                wh1.LiShiYuanYuan = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 18)//基本内容
                                wh1.JiBenNeiRong = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 19)//相关制品及其作品
                                wh1.XiangGuanZhiPinJiQiZuoPin = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 20)//传承谱系
                                wh1.ChuanChengPuXi = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 21)//代表性传承人
                                wh1.DaiBiaoXingChuanChengRen = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 22)//主要特征
                                wh1.ZhuYaoTeZheng = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 23)//重要价值
                                wh1.ZhongYaoJiaZhi = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 24)//存续状况
                                wh1.CunXuZhuangKuang = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 25)//主要传承人
                                wh1.ZhuYaoChuanChengRen = dt.Rows[i][j].ToString().Replace("'", "\"");
                            //if (j == 26)//项目名称 重复
                            //wh1.Type = dt.Rows[i][j].ToString().Replace("'","\"");
                            if (j == 27)//类型-图片数组
                                wh1.Type = wh1.Type + "," + dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 28)
                                wh1.Type = wh1.Type + "," + dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 29)
                                wh1.Type = wh1.Type + "," + dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 30)
                                wh1.Type = wh1.Type + "," + dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 31)
                                wh1.Type = wh1.Type + "," + dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 32)//主要传承人记录类型-视频
                                wh1.Other1 = dt.Rows[i][j].ToString().Replace("'", "\"");//TypeVideo
                            if (j == 33)//省
                                wh1.CoverageSpatial_Province = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 34)//市
                                wh1.CoverageSpatial_City = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 35)//区
                                wh1.CoverageSpatial_County = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 36)//申报地区或单位
                                wh1.CoverageSpatial_ShenBaoDiQuHuoDanWei = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 37)//保护单位
                                wh1.CoverageSpatial_BaoHuDanWei = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 38)//生态区项目
                                wh1.CoverageSpatial_ShengTaiQuXiangMu = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 39)//生产性保护示范基地
                                wh1.CoverageSpatial_ShengChanXingBaoHuShiFanJiDi = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 40)//备注暂时写 语言
                                wh1.Other2 = dt.Rows[i][j].ToString().Replace("'", "\"");//Yuyan
                            if (j == 41)//源资料创建者  名称
                                wh1.Source_CreatorOfReferences_Name = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 42)//责任方式
                                wh1.Source_CreatorOfReferences_Role = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 43)//源资料提供者
                                wh1.Source_ProviderOfReferences = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 44)//源资料典藏单位
                                wh1.Source_RepositoryName = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 45)//数字化格式
                                wh1.DigitalObjectInformation_DigitalObjectFormat = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 46)//大小
                                wh1.DigitalObjectInformation_Size = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 47)//时长
                                wh1.DigitalObjectInformation_Duration = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 48)//分辨率（解析度）
                                wh1.DigitalObjectInformation_DigitalSpecification = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 49)//音/视频数据码率
                                wh1.DigitalObjectInformation_AudioVideo = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 50)//音频采样频率
                                wh1.DigitalObjectInformation_AudioSamplingFrequency = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 51)//声道数
                                wh1.DigitalObjectInformation_NumberOfChannels = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 52)//储存地址
                                wh1.DigitalObjectInformation_FilePath = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 53)//显示级别
                                wh1.DigitalObjectInformation_DisplayType = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 54)//著录者
                                wh1.RecordingInformation_Recorder = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 55)//著录时间
                                wh1.RecordingInformation_RecordingTime = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 56)//审核者
                                wh1.RecordingInformation_MetadataAuditor = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 57)//审核时间
                                wh1.RecordingInformation_MetadataAuditoTime = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 58)//管理机构
                                wh1.RecordingInformation_MetadataManagement = dt.Rows[i][j].ToString().Replace("'", "\"");
                            if (j == 59)//备注
                                wh1.RecordingInformation_Note = dt.Rows[i][j].ToString().Replace("'", "\"");
                        }
                        //遍历excel每行 插入数据
                        var insertSql = @"INSERT INTO ";
                        insertSql += category.DataTableName + "(";
                        //遍历需要insert的字段
                        foreach (var item in category.CategoryFields.Where(x => x.CanModify == 1))
                        {
                            insertSql += item.IdEntity + ",";
                        }
                        insertSql += "InputManager,InputTime,IsRelease,IsExamine";
                        insertSql += ") VALUES (";
                        //根据遍历的字段放入对应的值
                        //foreach (var item in category.CategoryFields.Where(x => x.CanModify == 1))
                        //{
                        insertSql += "'" + wh1.ArtificialId + "'," + "'" + wh1.TitleProper + "'," + "'" + wh1.XiangMuMingCheng + "'," + "'" + wh1.FirstLevel + "'," + "'" + wh1.SecondLevel + "'," + "'" + wh1.XiangMuBianMa + "'," +
                            "'" + wh1.ZiXiangXuHao + "'," + "'" + wh1.PiCi + "'," + "'" + wh1.Annotation + "'," + "'" + wh1.XiangMuShenQingShiJian + "'," + "'" + wh1.MinZu + "'," + "'" + wh1.ZhiXi + "',"
                            + "'" + wh1.XiangMuJianJieBiaoZhunBan + "'," + "'" + wh1.XiangMuJianJie + "'," + "'" + wh1.SuoZaiQuYuJiQiDiLiHuanJing + "'," + "'" + wh1.FenBuQuYu + "'," + "'" + wh1.LiShiYuanYuan + "',"
                            + "'" + wh1.JiBenNeiRong + "'," + "'" + wh1.XiangGuanZhiPinJiQiZuoPin + "'," + "'" + wh1.ChuanChengPuXi + "'," + "'" + wh1.DaiBiaoXingChuanChengRen + "'," + "'" + wh1.ZhuYaoTeZheng + "',"
                            + "'" + wh1.ZhongYaoJiaZhi + "'," + "'" + wh1.CunXuZhuangKuang + "'," + "'" + wh1.ZhuYaoChuanChengRen + "'," + "'" + wh1.Type + "'," + "'" + wh1.Other1 + "'," + "'" + wh1.CoverageSpatial_Province + "',"
                            + "'" + wh1.CoverageSpatial_City + "'," + "'" + wh1.CoverageSpatial_County + "'," + "'" + wh1.CoverageSpatial_ShenBaoDiQuHuoDanWei + "'," + "'" + wh1.CoverageSpatial_BaoHuDanWei + "'," + "'" + wh1.CoverageSpatial_ShengTaiQuXiangMu + "'," + "'" + wh1.CoverageSpatial_ShengChanXingBaoHuShiFanJiDi + "',"
                            + "'" + wh1.Other2 + "'," + "'" + wh1.Source_CreatorOfReferences_Name + "'," + "'" + wh1.Source_CreatorOfReferences_Role + "'," + "'" + wh1.Source_ProviderOfReferences + "'," + "'" + wh1.Source_RepositoryName + "'," + "'" + wh1.DigitalObjectInformation_DigitalObjectFormat + "',"
                            + "'" + wh1.DigitalObjectInformation_Size + "'," + "'" + wh1.DigitalObjectInformation_Duration + "'," + "'" + wh1.DigitalObjectInformation_DigitalSpecification + "'," + "'" + wh1.DigitalObjectInformation_AudioVideo + "'," + "'" + wh1.DigitalObjectInformation_AudioSamplingFrequency + "'," + "'" + wh1.DigitalObjectInformation_NumberOfChannels + "',"
                            + "'" + wh1.DigitalObjectInformation_FilePath + "'," + "'" + wh1.DigitalObjectInformation_DisplayType + "'," + "'" + wh1.RecordingInformation_Recorder + "'," + "'" + wh1.RecordingInformation_RecordingTime + "'," + "'" + wh1.RecordingInformation_MetadataAuditor + "'," + "'" + wh1.RecordingInformation_MetadataAuditoTime + "',"
                            + "'" + wh1.RecordingInformation_MetadataManagement + "'," + "'" + wh1.RecordingInformation_Note + "',";
                        //if (item.IdEntity == "Title")
                        //{
                        title = wh1.TitleProper;
                        //}
                        //}
                        var admin = UserLogin.GetUserInfo("SESSION_USER_INFO");
                        insertSql += admin.ManagerId + ",'" + DateTime.Now + "','0','0') SELECT @@IDENTITY";

                        var reader = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, insertSql, null);
                        _systemLogService.EntryLog(admin.UserName, admin.ManagerId, "创建条目-" + title, "创建条目-" + title, 0);
                    }//遍历excel每行
                }
                return Content("<script>alert('导入成功!');</script>");
            }
            //如果模板不存在
            var m = UserLogin.GetUserInfo("SESSION_USER_INFO");
            _systemLogService.EntryLog(m.UserName, m.ManagerId, "创建条目-" + title, "创建条目-" + title, 0);
            return Content("<script>alert('数据错误，导入失败！');</script>");
            //return RedirectToAction("List");
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