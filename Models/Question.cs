using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GoEdu.Interface;

namespace GoEdu.Models
{
    
    public class Question : IDeleted
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public string ModelAnswer {  get; set; }
        [ForeignKey("Lecture")]
        public int LectureId {  get; set; }
        public bool isDeleted { get; set; } = false;


        public virtual Lecture? Lecture { get; set; }
        public virtual List<Answer>? Answer { get; set; }

        public virtual List<Option>? Options { get; set; }

    }
}
