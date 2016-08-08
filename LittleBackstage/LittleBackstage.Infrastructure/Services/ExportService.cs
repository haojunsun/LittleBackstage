using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;


namespace LittleBackstage.Infrastructure.Services
{
    public interface IExportService : IDependency
    {
        /// <summary>
        /// 输出为Excel(xlsx)格式。
        /// </summary>
        /// <param name="data">POCO对象，列名即为字段名。</param>
        /// <returns>下载文件路径</returns>
        Stream ToExcel(IEnumerable<object> data);

        /// <summary>
        /// 输出为Excel(xlsx)格式。
        /// </summary>
        /// <param name="data">POCO对象，列名即为字段名。</param>
        /// <param name="fields">逗号分隔的字段名，只输出指定字段。</param>
        /// <returns>下载文件路径</returns>
        Stream ToExcel(IEnumerable<object> data, string fields);
    }

    public class ExportService : IExportService
    {
        public Stream ToExcel(IEnumerable<object> data)
        {
            var pInfo = data.First().GetType().GetProperties();
            string fields = string.Join(",", pInfo.Select(x => x.Name));

            return ToExcel(data, fields);
        }

        public Stream ToExcel(IEnumerable<object> data, string fields)
        {
            var list = data.ToList();
            var workbook = new HSSFWorkbook();

            var sheet = workbook.CreateSheet("Sheet1");
            var row = sheet.CreateRow(0);

            var pInfo = fields.Split(',');

            for (int i = 0; i < pInfo.Length; i++)
            {
                var cell = row.CreateCell(i, CellType.String);
                cell.SetCellValue(pInfo[i]);
            }

            for (var i = 0; i < list.Count; i++)
            {
                var row2 = sheet.CreateRow(i + 1);
                for (var j = 0; j < pInfo.Length; j++)
                {
                    var cell2 = row2.CreateCell(j, CellType.String);
                    if (list[i].GetType().GetProperty(pInfo[j]).GetValue(list[i], null) != null)
                    {
                        //enum
                        if (list[i].GetType().GetProperty(pInfo[j]).GetValue(list[i], null).GetType().IsEnum)
                        {
                            var enumType = list[i].GetType().GetProperty(pInfo[j]).GetValue(list[i], null).GetType();
                            string name = Enum.GetName(enumType, list[i].GetType().GetProperty(pInfo[j]).GetValue(list[i], null));
                            var fieldInfo = enumType.GetField(name);
                            var attr = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                            cell2.SetCellValue(attr.Description);
                        }
                        else
                        {
                            cell2.SetCellValue(list[i].GetType().GetProperty(pInfo[j]).GetValue(list[i], null).ToString());
                        }
                    }
                    else
                    {
                        cell2.SetCellValue("");
                    }
                }
            }

            var fs = new MemoryStream();
            workbook.Write(fs);
            fs.Position = 0;
            return fs;
        }
    }
}
