using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GoEdu.Interface;

namespace GoEdu.Models
{
    public class Answer : IDeleted
    {
        [Key]  
        public int Id { get; set; }
        //محزوفه 
        //[ForeignKey("Exam")]
        //public int ExamId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public string? StudentAnswer { get; set; }
        public bool isDeleted { get; set; } = false;

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        //محزوفه 
        //[Required]
        //public virtual Exams? Exam { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Question? Question { get; set; }
        
    }
}
