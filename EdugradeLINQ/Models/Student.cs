using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EdugradeLINQ.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        [Display(Name = "Förnamn")]
        [Required]
        public string StudentFirstName { get; set; } = string.Empty;
        [Display(Name = "Efternamn")]
        [Required]
        public string StudentLastName { get; set; } = string.Empty;

        [ForeignKey("SchoolClass")]
        public int FkSchoolClassId { get; set; }
        public SchoolClass? SchoolClass { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }

    }
}
