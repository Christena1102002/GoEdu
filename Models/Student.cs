using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GoEdu.Interface;

namespace GoEdu.Models
{
    public class Student: IDeleted
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Name Must be less than 50 char")]
        [MinLength(2, ErrorMessage = "Name Must be More than 2 char")]
        public string Name { get; set; }

        [RegularExpression(@"\w+\.(com)")]
        public string Email { get; set; }

        [Range(1, 100, ErrorMessage = "Invalid Age, must be positive Number or less than 100!")]
        public int Age { get; set; }
        public string? Address { get; set; }

        [RegularExpression(@"^\\+?[0-9][0-9]{7,14}$",ErrorMessage ="Invalid Phone Number")]
        public string StudentPhone { get; set; }
        public bool isDeleted { get; set; } = false;
        public string ImageUrl { get; set; }
        public string ApplicationUserId { get; set; }
        // relation with application user
        public ApplicationUser ApplicationUser { get; set; }
        public virtual List<Enroll>? Enrolls { get; set; }
        public virtual List<Attend>? Attend { get; set; }    
        public virtual List<Comment>? Comment { get; set; }
        public virtual List<Answer>? Answers { get; set; }
        public virtual List<StudentPerformance>? StudentPerformances { get; set; }
    }
   

}
