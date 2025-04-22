using System.ComponentModel.DataAnnotations;
using GoEdu.Interface;

namespace GoEdu.Models
{
    public class Course: IDeleted
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Hours { get; set; }
        public int MaxViews { get; set; }
        public double Degree { get; set; }
        public double MinDegree { get; set; }
        public Semester Semester { get; set; }//=new Semester();
        public Stage StudentStage { get; set; }//=new Stage();
        public Level CourseLevel { get; set; }//=new Level();
        public int InstructorID { get; set; }
        public bool isDeleted { get; set; } = false;
        public string? ImageUrl { get; set; }

        public virtual List<Lecture>? Lecture { get; set; }
        public virtual List<Enroll>? Register { get; set; }
        public virtual Instructor? Instructor { get; set; }

    }

    public enum Semester
    {
        [Display(Name = "الأول")]
        First,

        [Display(Name = "الثاني")]
        Second,
    }

    public enum Stage
    {
        [Display(Name = "أعدادي")]
        Preparatory,

        [Display(Name = "ثانوي")]
        Secondary,
    }

    public enum Level
    {
        [Display(Name = "الأول")]
        First,

        [Display(Name = "الثاني")]
        Second,

        [Display(Name = "الثالت")]
        Third
    }

}


