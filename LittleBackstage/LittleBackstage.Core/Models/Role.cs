using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBackstage.Core.Models
{
   public class Role
    {
       public int RoleId { get; set; }

       public string RoleName { get; set; }

       public DateTime? CreateTime { get; set; }

       /// <summary>
       /// 角色类型 0 管理员 1 会员
       /// </summary>
       public int RoleType { get; set; }
       /// <summary>
       /// 权限
       /// </summary>
       public string Permissions { get; set; }
    }
}
