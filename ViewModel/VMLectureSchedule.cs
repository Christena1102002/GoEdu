using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoEdu.ViewModel
{
    public class VMLectureSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "عنوان المحاضرة مطلوب")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "يجب أن يكون عنوان المحاضرة بين 4 و200 حرف")]
        [Display(Name = "عنوان المحاضرة")]
        public string Title { get; set; }

        [Required(ErrorMessage = "اسم الكورس مطلوب")]
        [MaxLength(50, ErrorMessage = "يجب ألا يزيد اسم الكورس عن 50 حرفًا")]
        [MinLength(2, ErrorMessage = "يجب ألا يقل اسم الكورس عن حرفين")]
        [Display(Name = "اسم الكورس")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "وقت المحاضرة مطلوب")]
        [DataType(DataType.DateTime)]
        [Display(Name = "وقت المحاضرة")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime LectureTime { get; set; }

        [StringLength(2000, ErrorMessage = "لا يمكن أن يتجاوز الوصف 2000 حرف")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "الوصف")]
        public string? Description { get; set; }

    }
}
