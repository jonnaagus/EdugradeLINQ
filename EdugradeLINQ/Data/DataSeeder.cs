using EdugradeLINQ.Models;
using Microsoft.EntityFrameworkCore;

namespace EdugradeLINQ.Data
{
    public static class DataSeeder
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SchoolDbContext>();

                context.Database.Migrate();

                SeedTeachers(context);
                SeedSchoolClasses(context);
                SeedCourses(context);
                SeedStudents(context);
                SeedEnrollments(context);
            }
        }

        private static void SeedTeachers(SchoolDbContext context)
        {
            if (!context.Teachers.Any())
            {
                context.Teachers.AddRange(new List<Teacher>
                {
                    new Teacher { TeacherFirstName = "Reidar", TeacherLastName = "Nilsen" },
                    new Teacher { TeacherFirstName = "Tobias", TeacherLastName = "Landén" },
                    new Teacher { TeacherFirstName = "Aldor", TeacherLastName = "Besher" },
                    new Teacher { TeacherFirstName = "Marina", TeacherLastName = "Holm" },
                    new Teacher { TeacherFirstName = "Rebecka", TeacherLastName = "Rehn" }
                });
                context.SaveChanges();
            }
        }

        private static void SeedSchoolClasses(SchoolDbContext context)
        {
            if (!context.SchoolClasses.Any())
            {
                var teachers = context.Teachers.ToList();

                context.SchoolClasses.AddRange(new List<SchoolClass>
                {
                    new SchoolClass { ClassName = ".NET23", Teacher = teachers.FirstOrDefault(t => t.TeacherFirstName.Contains("Reidar")) },
                    new SchoolClass { ClassName = ".NET24", Teacher = teachers.FirstOrDefault(t => t.TeacherFirstName.Contains("Aldor")) }
                });
                context.SaveChanges();
            }
        }

        private static void SeedCourses(SchoolDbContext context)
        {
            if (!context.Courses.Any())
            {
                context.Courses.AddRange(new List<Course>
                {
                    new Course { CourseName = "Programmering 1" },
                    new Course { CourseName = "Programmering 2" }
                });
                context.SaveChanges();
            }
        }

        private static void SeedStudents(SchoolDbContext context)
        {
            if (!context.Students.Any())
            {
                var schoolClasses = context.SchoolClasses.ToList();

                context.Students.AddRange(new List<Student>
                {
                    new Student { StudentFirstName = "Klara", StudentLastName = "Mörk", SchoolClass = schoolClasses.FirstOrDefault(sc => sc.ClassName.Contains(".NET23")) },
                    new Student { StudentFirstName = "Hannes", StudentLastName = "Björklund", SchoolClass = schoolClasses.FirstOrDefault(sc => sc.ClassName.Contains(".NET24")) },
                    new Student { StudentFirstName = "Leona", StudentLastName = "Sandén", SchoolClass = schoolClasses.FirstOrDefault(sc => sc.ClassName.Contains(".NET23")) },
                    new Student { StudentFirstName = "Maria", StudentLastName = "Klasson", SchoolClass = schoolClasses.FirstOrDefault(sc => sc.ClassName.Contains(".NET24")) },
                    new Student { StudentFirstName = "Ted", StudentLastName = "Björk", SchoolClass = schoolClasses.FirstOrDefault(sc => sc.ClassName.Contains(".NET23")) },
                    new Student { StudentFirstName = "Hampus", StudentLastName = "Jansson", SchoolClass = schoolClasses.FirstOrDefault(sc => sc.ClassName.Contains(".NET24")) },
                    new Student { StudentFirstName = "Selina", StudentLastName = "Fransén", SchoolClass = schoolClasses.FirstOrDefault(sc => sc.ClassName.Contains(".NET23")) },
                    new Student { StudentFirstName = "Ulrik", StudentLastName = "Ingemarsson", SchoolClass = schoolClasses.FirstOrDefault(sc => sc.ClassName.Contains(".NET24")) },
                    new Student { StudentFirstName = "Philippa", StudentLastName = "Ring", SchoolClass = schoolClasses.FirstOrDefault(sc => sc.ClassName.Contains(".NET23")) }
                });
                context.SaveChanges();
            }
        }

        private static void SeedEnrollments(SchoolDbContext context)
        {
            if (!context.Enrollments.Any())
            {
                var students = context.Students.ToList();
                var courses = context.Courses.ToList();
                var teachers = context.Teachers.ToList();

                context.Enrollments.AddRange(new List<Enrollment>
                {
                    new Enrollment { Student = students.FirstOrDefault(s => s.StudentFirstName.Contains("Klara")), Course = courses.FirstOrDefault(c => c.CourseName.Contains("Programmering 1")), Teacher = teachers.FirstOrDefault(t => t.TeacherFirstName.Contains("Reidar")) },
                    new Enrollment { Student = students.FirstOrDefault(s => s.StudentFirstName.Contains("Hannes")), Course = courses.FirstOrDefault(c => c.CourseName.Contains("Programmering 1")), Teacher = teachers.FirstOrDefault(t => t.TeacherFirstName.Contains("Reidar")) },
                    new Enrollment { Student = students.FirstOrDefault(s => s.StudentFirstName.Contains("Leona")), Course = courses.FirstOrDefault(c => c.CourseName.Contains("Programmering 2")), Teacher = teachers.FirstOrDefault(t => t.TeacherFirstName.Contains("Aldor")) },
                    new Enrollment { Student = students.FirstOrDefault(s => s.StudentFirstName.Contains("Maria")), Course = courses.FirstOrDefault(c => c.CourseName.Contains("Programmering 2")), Teacher = teachers.FirstOrDefault(t => t.TeacherFirstName.Contains("Aldor")) },
                    new Enrollment { Student = students.FirstOrDefault(s => s.StudentFirstName.Contains("Ted")), Course = courses.FirstOrDefault(c => c.CourseName.Contains("Programmering 1")), Teacher = teachers.FirstOrDefault(t => t.TeacherFirstName.Contains("Reidar")) },
                    new Enrollment { Student = students.FirstOrDefault(s => s.StudentFirstName.Contains("Hampus")), Course = courses.FirstOrDefault(c => c.CourseName.Contains("Programmering 1")), Teacher = teachers.FirstOrDefault(t => t.TeacherFirstName.Contains("Reidar")) },
                    new Enrollment { Student = students.FirstOrDefault(s => s.StudentFirstName.Contains("Selina")), Course = courses.FirstOrDefault(c => c.CourseName.Contains("Programmering 2")), Teacher = teachers.FirstOrDefault(t => t.TeacherFirstName.Contains("Aldor")) },
                    new Enrollment { Student = students.FirstOrDefault(s => s.StudentFirstName.Contains("Ulrik")), Course = courses.FirstOrDefault(c => c.CourseName.Contains("Programmering 2")), Teacher = teachers.FirstOrDefault(t => t.TeacherFirstName.Contains("Aldor")) },
                    new Enrollment { Student = students.FirstOrDefault(s => s.StudentFirstName.Contains("Philippa")), Course = courses.FirstOrDefault(c => c.CourseName.Contains("Programmering 1")), Teacher = teachers.FirstOrDefault(t => t.TeacherFirstName.Contains("Reidar")) }
                });
                context.SaveChanges();
            }
        }
    }
}
