using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBackstage.Core.Models
{
    public class SystemLog
    {
        public int SystemLogId { get; set; }
        /// <summary>
        /// log 类型
        /// </summary>
        public int LogType { get;set;}
        public string LogUserName{ get;set;}
        public string LogUserId{ get;set;}
        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperateType{ get;set;}
        public DateTime? LogTime { get;set;}
        /// <summary>
        /// 操作详情
        /// </summary>
        public string LogDetails{ get;set;}
    }
}
