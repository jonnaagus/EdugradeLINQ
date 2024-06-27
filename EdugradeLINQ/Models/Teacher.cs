using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EdugradeLINQ.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }

        [Display(Name = "Förnamn")]
        public string TeacherFirstName { get; set; } = string.Empty;

        [Display(Name = "Efternamn")]
        public string TeacherLastName { get; set; } = string.Empty;

        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}
