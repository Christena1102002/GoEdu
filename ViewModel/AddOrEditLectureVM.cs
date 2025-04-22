using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoEdu.ViewModel
{
    public class AddOrEditLectureVM
    {
        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "العنوان بين 5 ل 200 حرف")]
        [Display(Name ="عنوان الحصة")]
        public string Title { get; set; }

        //[Url(ErrorMessage = "غير صالح URL format")]
        //[Required(ErrorMessage = "هذا الحقل مطلوب")]
        //[StringLength(500, ErrorMessage = "لا يجب ان يتجاوز 500 حرف URL")]
        //[Display(Name = "Video URL")]
        //public string? VideoURL { get; set; } 

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.DateTime)]
        [Display(Name = "ميعاد الحصة")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime LectureTime { get; set; }

        [StringLength(2000, ErrorMessage = "يجب ان يكون اقل من 2000 حرف")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "الوصف")]
        public string? Description { get; set; }
        public string VideoURL { get; set; }
        public int LctID { get; set; }

        public int CourseID { get; set; }

    }
}
