namespace GoEdu.ViewModel
{
    public class LectureWithInstructorVM
    {
        public int LctID { get; set; }
        public string Title { get; set; }
        public DateTime LctTime { get; set; }
        public string? Description { get; set; }
        public int CrsID { get; set; }
        public string VideoURL { get; set; }

        public int LectureCount { get; set; }
    }
}
