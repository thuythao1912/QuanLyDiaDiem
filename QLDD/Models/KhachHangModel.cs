using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLDD.Models
{
    public class KhachHangModel
    {
        [Key]
        public string ID { set; get; }
        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage ="Vui lòng nhập tài khoản")]
        public string TK { set; get; }
        [Display(Name ="Mật khẩu")]
        [Required(ErrorMessage ="Vui lòng nhập Mật khẩu")]
        public string MK { set; get; }
        [Display(Name ="Họ tên")]
        public string Ten { set; get; }

    }
}