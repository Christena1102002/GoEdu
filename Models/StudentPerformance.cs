using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GoEdu.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoEdu.Models
{
    [PrimaryKey(nameof(StudentId), nameof(LectureId))]
    public class StudentPerformance: IDeleted
    {
        [Range(0,100)]
        public int Grade { get; set; }
        public bool isDeleted { get; set; } = false;

        [ForeignKey("Student")]
        public int StudentId {  get; set; }

        [ForeignKey("Lecture")]
        public int LectureId {  get; set; }

        public virtual Student? Student { get; set; }
        public virtual Lecture? Lecture { get; set; }
    }
}
