//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp2020.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Teacher
    {
        public string TeacherID { get; set; }
        public string TeacharName { get; set; }
        public string Password { get; set; }
        public string Sex { get; set; }
        public Nullable<int> MajorID { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public string REMARK { get; set; }
    
        public virtual YzuMajor YzuMajor { get; set; }
        public virtual Sex Sex1 { get; set; }
        public virtual Title Title1 { get; set; }
    }
}