using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CollegeApp.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public int DeptSectionId { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateOfJoin { get; set; }
        public DateTime DateofGraduaton { get; set; }

        [ForeignKey("DeptSectionId")]
        public virtual DeptSection DeptSection1 { get; set; }
    }
}