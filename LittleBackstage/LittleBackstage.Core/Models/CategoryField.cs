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
        /// <summary>
        /// 
        /// </summary>
        public int CategoryFieldId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 标识 sql 字段
        /// </summary>
        public string IdEntity { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        public virtual Category Category { get; set; }

        /// <summary>
        /// 系统设置 默认为1 可以修改 特定字段为0 用户不可操作
        /// </summary>
        public int CanModify { get; set; }

        /// <summary>
        /// 是否显示在table 0 否 1是 
        /// </summary>
        public int IsShow { get; set; }
    }
}
