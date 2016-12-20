using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBackstage.Core.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否启用 0否 1是
        /// </summary>
        public int IsEnable { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; set; }

        /// <summary>
        /// 是否创建表（前端显示 是否创建模板）0否 1是
        /// </summary>
        public int IsCreateTable { get; set; }

        /// <summary>
        /// 分类表名(自动生成)
        /// </summary>
        public string DataTableName { get; set; }

        /// <summary>
        /// 字段集合
        /// </summary>
        public string DataTableFieldSet { get; set; }

        public virtual List<CategoryField> CategoryFields { get; set; }

        /// <summary>
        /// 是否删除 逻辑删除 1是 0否
        /// </summary>
        public int IsDelete { get; set; }
    }
}
