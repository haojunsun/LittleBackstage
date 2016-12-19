using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBackstage.Core.Models
{
    /// <summary>
    /// 分类字段
    /// </summary>
    public class CategoryField
    {
        public int CategoryFieldId { get; set; }

        public string FieldName { get; set; }

        /// <summary>
        /// 标识 sql 字段
        /// </summary>
        public string IdEntity { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
