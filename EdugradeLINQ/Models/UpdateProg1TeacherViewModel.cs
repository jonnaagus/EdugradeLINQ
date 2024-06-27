namespace EdugradeLINQ.Models
{
    public class UpdateProg1TeacherViewModel
    {
        public int SelectedStudentId { get; set; }
        public int SelectedTeacherId { get; set; }
        public List<Student> Students { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}
