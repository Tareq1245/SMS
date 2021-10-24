//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace WebApp2020.ViewModels
//{
    //public class AdminUserLoginEntity
    //{
    //}
//}


using System;
using System.Collections.Generic;
//using the following namespace for
//   model validation
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp2020.ViewModels
{
    public class AdminUserLoginEntity
    {
        [Key]
        [Display(Name = "UserID: ")]
        [Required(ErrorMessage = "UserID CANNOT be Empty!")]
        [System.Web.Mvc.Remote("CheckUserID", "Admin", ErrorMessage = "UserID does NOT exist!")]
        public string UserID { get; set; }

        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "Enter Correct Password")]
        //[StringLength(maximumLength:20, MinimumLength=6, ErrorMessage = "密码长度应该在6~20之间!")]
        [MaxLength(20, ErrorMessage = "Maximum Password Length 20!")]
        [MinLength(6, ErrorMessage = "Minimum Password Length 6!")]
        [DataType(DataType.Password)]
        [System.Web.Mvc.Remote("CheckPassword", "Admin", AdditionalFields = "UserID", ErrorMessage = "Password is error!")]
        public string password { get; set; }
    }
}