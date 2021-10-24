using System;
using System.Collections.Generic;
// using the following namespace for 
//   annotation ▼
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp2020.Models
{
    public class StudentDB2Validation
    {

    }
    //Alt+Enter key to show code suggestions,
    // Then Select the using ... (a certain namespace)
    // Here we select ""
    [MetadataType(typeof(AdminUser_Validation))]
    public partial class AdminUser
    {
    }

    [MetadataType(typeof(Teacher_Validation))]
    public partial class Teacher
    {
    }

    [MetadataType(typeof(Student_Validation))]
    public partial class Student
    {
    }
    public class AdminUser_Validation
    {
        [Display(Name = "UserID: ")]
        [Required(ErrorMessage = "Enter Valid UserId")]
        public string UserID { get; set; }

        [Display(Name = "User Name: ")]
        [Required(ErrorMessage = "Enter Valid User Name")]
        public string UserName { get; set; }

        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "Enter a valid password with length 6-20 ")]
        //[StringLength(20, MinimumLength = 6 ErrorMessage = "密码长度不能大于6!")]
        [MaxLength(20, ErrorMessage = "The length should not be ≧ 20.")]
        [MinLength(6, ErrorMessage = "The length should not be ≦ 6.")]
        [DataType(DataType.Password)]
        //[StringLength(6, ErrorMessage = "The Password Length Should be less than 6!")]
        public string password { get; set; }

        //we cannot found ConfirmPassword here!
        // both in model class/validaton class/database table

        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Enter a Valid Email..")]
        
        [RegularExpression("\\w+([.-]?\\w+)*@\\w+(\\.\\w+)*\\.[a-z]{2,3}", ErrorMessage = "Email address is error!")]
        
        public string Email { get; set; }
    }

    public class Teacher_Validation
    {
        [Display(Name = "TeacherID: ")]
        [Required(ErrorMessage = "Enter Valid TeacherId..")]
        public string TeacherID { get; set; }
        [Display(Name = "Teacher Name: ")]
        [Required(ErrorMessage = "Enter Valid Teacher Name..")]
        public string TeacharName { get; set; }
        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "Enter a valid password with length 6-20 ")]
        [MaxLength(20, ErrorMessage = "The length should not be ≧ 20.")]
        [MinLength(6, ErrorMessage = "The length should not be ≦ 6.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Sex: ")]
        public string Sex { get; set; }

        public Nullable<int> MajorID { get; set; }
        
        public string Title { get; set; }

        public string Photo { get; set; }
        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Enter a Valid Email..")]
        [RegularExpression("\\w+([.-]?\\w+)*@\\w+(\\.\\w+)*\\.[a-z]{2,3}", ErrorMessage = "Email address is error!")]
        public string Email { get; set; }
        [Display(Name = "Remark: ")]
        [Required(ErrorMessage = "Enter a Valid Remark..")]
        public string REMARK { get; set; }

        public virtual YzuMajor YzuMajor { get; set; }
        [Display(Name ="Sex: ")]
        public virtual Sex Sex1 { get; set; }
        [Display(Name ="Title: ")]
        public virtual Title Title1 { get; set; }
    }

    public class Student_Validation
    {
        [Display(Name = "StudentID: ")]
        [Required(ErrorMessage = "Enter Valid StudentId")]
        public string StudentID { get; set; }
        [Display(Name = "StudentName: ")]
        [Required(ErrorMessage = "Enter Valid StudentName..")]
        public string StudentName { get; set; }
        [Display(Name = "Sex: ")]
        [Required(ErrorMessage = "Please Enter Sex as M for Male and F for Female")]
        public string Sex { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public Nullable<int> MajorID { get; set; }
        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Enter a Valid Email..")]
        [RegularExpression("\\w+([.-]?\\w+)*@\\w+(\\.\\w+)*\\.[a-z]{2,3}", ErrorMessage = "Email address is error!")]
        public string Email { get; set; }
        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "Enter a valid password with length 6-20 ")]
        [MaxLength(20, ErrorMessage = "The length should not be ≧ 20.")]
        [MinLength(6, ErrorMessage = "The length should not be ≦ 6.")]
        [DataType(DataType.Password)]
        public string Pwd { get; set; }
        public string Photo { get; set; }
    }
}