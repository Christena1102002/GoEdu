using GoEdu.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GoEdu.ViewModel
{
    public class AddCourseWithInstructorVM
    {
        public int CrsID { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [MinLength(3, ErrorMessage = "اسم الكورس لا يجب ان يقل عن 3 احرف")]
        [MaxLength(50, ErrorMessage = "اسم الكورس لا يزيد عن 50 حرف")]
        [Display(Name = "أسم الكورس")]
        public string CrsName { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Remote("CheckPrice", "Course", ErrorMessage = "السعر لا يقل عن 50")]
        [Display(Name = "السعر")]
        public double CrsPrice { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Range(1, 10000, ErrorMessage = "عدد الساعات لا يقل عن 1")]
        [Display(Name = "عدد الساعات")]
        public int CrsHours { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Range(1, 90, ErrorMessage = "الحد الادني للدرجات بين 1 و 90")]
        [Display(Name = "الحد الأدني للدرجات")]
        public double CrsMinDegree { get; set; }
        
        [Display(Name = "لينك صورة الكورس")]
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الفصل الدراسي")]
        public Semester CrsSemester { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "المرحلة التعليمية")]
        public Stage CrsStage { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الصف الدراسي")]
        public Level CrsLevel { get; set; }

        public int InsID { get; set; }
    }
}
