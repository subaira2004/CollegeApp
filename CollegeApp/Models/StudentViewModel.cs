using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CollegeApp.Models
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        [Required]
        public int DeptSectionId { get; set; }
        public string SectionName { get; set; }
        [Required]
        public string StudentName { get; set; }
        public Nullable<DateTime> DateOfJoin { get; set; }
        public Nullable<DateTime> DateofGraduaton { get; set; }
    }
}