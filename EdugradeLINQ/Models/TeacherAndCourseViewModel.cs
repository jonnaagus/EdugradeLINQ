using System.ComponentModel.DataAnnotations;

namespace EdugradeLINQ.Models
{
    public class TeacherAndCourseViewModel
    {
        [Display(Name = "Kurs")]
        public string CourseName { get; set; }
        [Display(Name = "Lärare")]
        public string TeacherName { get; set; }

        public TeacherAndCourseViewModel(string courseName, string teacherFirstName, string teacherLastName)
        {
            CourseName = courseName;
            TeacherName = $"{teacherFirstName} {teacherLastName}";
        }
    }
}
