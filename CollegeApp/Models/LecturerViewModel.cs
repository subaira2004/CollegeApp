using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CollegeApp.Models
{
    public class LecturerViewModel
    {
        public int LecturerId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public string LecturerName { get; set; }
        public string DepartmentName { get; set; }

    }
}