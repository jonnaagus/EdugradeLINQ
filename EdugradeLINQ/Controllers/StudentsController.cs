using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EdugradeLINQ.Data;
using EdugradeLINQ.Models;

namespace EdugradeLINQ.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolDbContext _context;

        public StudentsController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var schoolDbContext = _context.Students.Include(s => s.SchoolClass);
            return View(await schoolDbContext.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> UpdateTeacherForStudentProg1()
        {
            var viewModel = new UpdateProg1TeacherViewModel
            {
                Students = await _context.Students
                    .Where(s => s.Enrollments.Any(e => e.Course.CourseName == "Programmering 1"))
                    .ToListAsync(),
                Teachers = await _context.Teachers.ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTeacherForStudentProg1(int selectedStudentId, int selectedTeacherId)
        {
            var enrollmentToUpdate = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.FkStudentId == selectedStudentId && e.Course.CourseName == "Programmering 1");

            if (enrollmentToUpdate == null)
            {
                return NotFound();
            }

            var selectedTeacher = await _context.Teachers.FindAsync(selectedTeacherId);
            if (selectedTeacher == null)
            {
                return NotFound();
            }

            enrollmentToUpdate.Teacher = selectedTeacher;
            await _context.SaveChangesAsync();

            ViewData["Message"] = $"Läraren har uppdaterats till {selectedTeacher.TeacherFirstName} {selectedTeacher.TeacherLastName}";

            var viewModel = new UpdateProg1TeacherViewModel
            {
                Students = await _context.Students
                    .Where(s => s.Enrollments.Any(e => e.Course.CourseName == "Programmering 1"))
                    .ToListAsync(),
                Teachers = await _context.Teachers.ToListAsync(),
                SelectedStudentId = selectedStudentId,
                SelectedTeacherId = selectedTeacherId
            };

            return View(viewModel);
        }

        public async Task<IActionResult> StudentsInProgramming1()
        {
            var studentsInProg1 = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .Where(s => s.Enrollments.Any(e => e.Course.CourseName == "Programmering 1"))
                .ToListAsync();

            if (studentsInProg1 == null || studentsInProg1.Count == 0)
            {
                return NotFound();
            }

            return View(studentsInProg1);
        }


        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.SchoolClass)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["FkSchoolClassId"] = new SelectList(_context.SchoolClasses, "SchoolClassId", "ClassName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudentFirstName,StudentLastName,FkSchoolClassId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkSchoolClassId"] = new SelectList(_context.SchoolClasses, "SchoolClassId", "ClassName", student.FkSchoolClassId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["FkSchoolClassId"] = new SelectList(_context.SchoolClasses, "SchoolClassId", "ClassName", student.FkSchoolClassId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentFirstName,StudentLastName,FkSchoolClassId")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkSchoolClassId"] = new SelectList(_context.SchoolClasses, "SchoolClassId", "ClassName", student.FkSchoolClassId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.SchoolClass)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
