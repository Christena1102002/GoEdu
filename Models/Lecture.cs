using System.ComponentModel.DataAnnotations.Schema;
using GoEdu.Interface;
using System.ComponentModel.DataAnnotations;



namespace GoEdu.Models
{

    public class Lecture : IDeleted
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }
        public string VideoURL { get; set; }
        public DateTime LectureTime { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }

        public bool isDeleted { get; set; } = false;
        public int CourseID { get; set; }

        [Display(Name = "سجلات الحضور")]
        public virtual List<Attend>? Attend { get; set; }

        [Display(Name = "التعليقات")]
        public virtual List<Comment>? Comment { get; set; }

        [Display(Name = "الأسئلة")]
        public virtual List<Question>? Question { get; set; }

        [ForeignKey("CourseID")]
        [Display(Name = "المقرر المرتبط")]
        public virtual Course? Course { get; set; }
    }
}
