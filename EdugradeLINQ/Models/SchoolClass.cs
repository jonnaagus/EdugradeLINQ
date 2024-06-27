using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EdugradeLINQ.Models
{
    public class SchoolClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SchoolClassId { get; set; }
        [Required]
        [Display(Name = "Klass")]
        public string ClassName { get; set; } = string.Empty;
        [ForeignKey("Teacher")]
        public int? FkTeacherId { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
