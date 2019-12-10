using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLDD.Areas.Admin.Models
{
    public class LoginModel
    {
        [Key]
        public int ID { set; get; }
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [Display(Name = "Mật khẩu")]
        public string Password { set; get; }
        public bool RememberMe { set; get; }
    }
}