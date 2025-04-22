using System.ComponentModel.DataAnnotations.Schema;
using GoEdu.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoEdu.Models
{
    [PrimaryKey(nameof(StudentID), nameof(LectureID))]
    public class Attend: IDeleted
    {

        public int StudentID { get; set; }
        public int LectureID { get; set; }
        public int ViewsCount { get; set; }

        //ملهاش لازمة دلوقتي عشان عملنا لوجيك جديد
        //public bool Status { get; set; }
        public bool isDeleted { get; set; } = false;

        [ForeignKey("StudentID")]
        public virtual Student? Student { get; set; }

        [ForeignKey("LectureID")]
        public virtual Lecture? Lecture { get; set; }
        


    }
}
