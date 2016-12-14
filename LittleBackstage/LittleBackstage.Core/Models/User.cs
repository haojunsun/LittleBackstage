using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBackstage.Core.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string NickName { get; set; }
        public string PassWord { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? Register { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; }
        /// <summary>
        /// 通过审核
        /// </summary>
        public int IsExamine { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual Role Role { get; set; }
    }
}
