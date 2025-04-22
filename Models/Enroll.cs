using System.ComponentModel.DataAnnotations.Schema;
using GoEdu.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoEdu.Models
{
    [PrimaryKey(nameof(StudentID), nameof(CourseID), nameof(InstructorID))]
    public class Enroll: IDeleted
    {
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public int InstructorID {  get; set; }

        public DateTime Data { get; set; }
        public string? Status { get; set; }
        public bool isDeleted { get; set; } = false;

        [ForeignKey("StudentID")]
        public virtual Student? Student { get; set; }

        [ForeignKey("CourseID")]
        public virtual Course? Course { get; set; }

        [ForeignKey("InstructorID")]
        public virtual Instructor? Instructor { get; set; }


    }
}
