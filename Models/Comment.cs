using System.ComponentModel.DataAnnotations.Schema;
using GoEdu.Interface;

namespace GoEdu.Models
{
    public class Comment: IDeleted
    {
        public int ID { get; set; }
        public int LectureID { get; set; }
        public int UserID { get; set; }
        public string? Content { get; set; }    
        public DateTime Date { get; set; }
        public bool isDeleted { get; set; } = false;

        [ForeignKey("UserID")]
        public virtual Student? Student { get; set; }

        [ForeignKey("LectureID")]
        public virtual Lecture? Lecture { get; set; }
        

    }
}
