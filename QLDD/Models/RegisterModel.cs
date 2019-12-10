using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLDD.Models
{
    public class RegisterModel
    {
        [Key]
        public int ID { set; get; }

        [Display(Name = "Tên")]
        public string TenKH { set; get; }

        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessage = "Tên tài khoản là bắt buộc")]
        public string TK { set; get; }

        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài ít nhất 6 kí tự")]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string MK { set; get; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("MK", ErrorMessage = "Xác nhận Mật khẩu không đúng")]
        public string ConfirmMK { set; get; }

        [Display(Name = "Số điện thoại")]
        public string SDT { set; get; }

        [Display(Name = "Email")]
        public string Email { set; get; }
        public string CaptchaCode { get; set; }
    }
}