using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Inspiration.Core.Data;

namespace Inspiration.Model
{
    public class User : BaseEntity
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "用户名为必填项")]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "是否启用")]
        public bool IsActive { get; set; }

        [Display(Name = "记录创建日期")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "最后停用日期")]
        public DateTime? LastStopUseDate { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
