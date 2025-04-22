using System.ComponentModel.DataAnnotations;

namespace GoEdu.ViewModel
{
    public class VMCourseList
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [Display(Name = "Course Name")]
        public string Name { get; set; }

    }
}
