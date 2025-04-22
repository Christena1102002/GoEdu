using Microsoft.AspNetCore.Identity;

namespace GoEdu.Models
{
    public class ApplicationUser:IdentityUser
    {
        public Student Student {  get; set; }
        public Instructor Instructor { get; set; }
    }
}
