using GoEdu.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoEdu.ViewModel
{
    public class VMLectureDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "العنوان مطلوب")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "يجب أن يكون العنوان بين 4 إلى 200 حرف")]
        [Display(Name = "عنوان المحاضرة")]
        public string Title { get; set; }

        [Required(ErrorMessage = "رابط الفيديو مطلوب")]
        [Url(ErrorMessage = "صيغة الرابط غير صحيحة")]
        [StringLength(500, ErrorMessage = "لا يمكن أن يتجاوز الرابط 500 حرف")]
        [Display(Name = "رابط الفيديو")]
        public string VideoURL { get; set; }

        [Required(ErrorMessage = "معاد الحصة مطلوب")]
        [DataType(DataType.DateTime)]
        [Display(Name = "ميعاد الحصة")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime LectureTime { get; set; }

        [StringLength(2000, ErrorMessage = "لا يمكن أن يتجاوز الوصف 2000 حرف")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "الوصف")]
        public string? Description { get; set; }
        public int ExamId { get; set; }
        
        //navigation properties
        // from attend
        [Display(Name = "عدد المشاهدات")]
        public int ViewsCount { get; set; }

        [Required]
        public string CommentContent {  get; set; }

        [Display(Name = "التعليقات")]
        public virtual List<VMComment>? Comments { get; set; }

        // from coure
        [Display(Name = "اسم الدورة")]
        public string CourseName { get; set; }
    }
}
