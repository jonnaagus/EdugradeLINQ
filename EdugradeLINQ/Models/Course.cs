using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EdugradeLINQ.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [Display(Name = "Kurs")]
        [Required]
        public string CourseName { get; set; } = string.Empty;

        public virtual ICollection<Enrollment>? Enrollments { get; set; }

    }
}
