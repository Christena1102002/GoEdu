namespace GoEdu.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class VMComment
    {
        public int UserId { get; set; }
        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }
        public string UserImageUrl { get; set; }    

        [StringLength(500, MinimumLength = 1, ErrorMessage = "يجب أن يكون المحتوى بين 5 و 500 حرف")]
        [Display(Name = "المحتوى")]
        public string Content { get; set; }

        [Display(Name = "تاريخ التعليق")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public int LectureId { get; set; }
    }

}
