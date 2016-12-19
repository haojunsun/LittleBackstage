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
        /// 分类表名(自动生成)
        /// </summary>
        public string DataTableName { get; set; }

        /// <summary>
        /// 字段集合
        /// </summary>
        public string DataTableFieldSet { get; set; }

    }
}
