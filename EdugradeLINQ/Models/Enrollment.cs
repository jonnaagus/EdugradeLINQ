using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EdugradeLINQ.Models
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentId { get; set; }

        [ForeignKey("Student")]
        [Display(Name = "Elevens Id")]
        public int FkStudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Course")]
        [Display(Name = "Kursens Id")]
        public int FkCourseId { get; set; }
        public Course Course { get; set; }

        [ForeignKey("Teacher")]
        [Display(Name = "Lärarens Id")]
        public int? FkTeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }

}
