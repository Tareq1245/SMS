using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp2020.ViewModels
{
    public class CourseGrade
    {
        [Key]
        public string StudentID { set; get; }
        public string StudentName { set; get; }
        //public string CourseID { set; get; }
        //public string CourseName { set; get; }
        public Nullable<short> Score { set; get; }
    }
}