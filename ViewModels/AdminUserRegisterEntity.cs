//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace WebApp2020.ViewModels
//{
    //public class AdminUserRegisterEntity
    //{
  //  }
//}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp2020.Controllers;

namespace WebApp2020.ViewModels
{
    public class AdminUserRegisterEntity
    {
        [Key]
        [Display(Name = "UserID: ")]
        [Required(ErrorMessage = "Make Sure Your User Id is Okay")]
        [System.Web.Mvc.Remote("CheckUserID4Register", "Admin", ErrorMessage = "The UserID exists, you can not use this UserID to register.")]
        public string UserID { get; set; }

        [Display(Name = "Name: ")]
        [Required(ErrorMessage = "Check the Name !")]
        public string UserName { get; set; }

        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "Error Password")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Password Length 6-20")]
        //[MaxLength(20, ErrorMessage = "密码长度不能大于20!")]
        //[MinLength(6, ErrorMessage = "密码长度不能小于6!")]
        [DataType(DataType.Password)]
        public string password { get; set; }


        //Add an additional ConfirmPassword property!
        [Display(Name = "Confirm Password: ")]
        [Compare("password", ErrorMessage = "Check Your Password Again it's not Matching")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Enter an valid EMail Id")]
        //[EmailAddress(ErrorMessage = "邮箱地址格式不对!abc@abc")]
        //[Email(ErrorMessage = "Email address is error!")]
        //[RegularExpression("/w[a-zA-Z_0-9]+([.-]?[a-zA-Z_0-9]+)*@[a-zA-Z_0-9]+(.[a-zA-Z_0-9]+)*/.[a-z]{2,3}", ErrorMessage = "Email address is error!")]

        //OK! ▼ 
        //[RegularExpression("\\w+([.-]?\\w+)*@\\w+(\\.\\w+)*\\.[a-z]{2,3}", ErrorMessage = "Email address is error!")]
        // Also OK! ▼  '@' is used for string literals without escape
        [RegularExpression(@"\w+([.-]?\w+)*@\w+(\.\w+)*\.[a-z]{2,3}", ErrorMessage = "Email address is error!")]
        //Remote validation
        [System.Web.Mvc.Remote("CheckEmail4Register", "Admin", ErrorMessage = "The Email exists, you CANNOT register with an EXISTING Email!")]
        public string Email { get; set; }
    }
}