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
        /// log 类型： 1 登录 2会员操作
        /// </summary>
        public int LogType { get;set;}
        public string LogUserName{ get;set;}
        public int LogUserId{ get;set;}
        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperateType{ get;set;}
        public DateTime? LogTime { get;set;}
        /// <summary>
        /// 操作详情
        /// </summary>
        public string LogDetails{ get;set;}

        /// <summary>
        /// 相关id 如 条目 模板等 id
        /// </summary>
        public int RelevantId { get; set; }
    }
}
