using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using University.API.Contexts;
using University.API.Entities;
using University.API.Interfaces;

namespace University.API.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly UniversityContext _context;

        public CourseRepository(UniversityContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public void AddLessonToCourse(int courseId, Lesson lesson)
        {
            var course = Get(courseId, false);
            course.Lessons.Add(lesson);

        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Course Get(int id, bool includeLessons)
        {
            if (includeLessons)
            {

                return _context.Courses.Include(c => c.Lessons).Where(c => c.Id == id).FirstOrDefault();
            }

            return _context.Courses.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.OrderBy(c => c.Name).ToList();
        }
         
        public Lesson GetLesson(int courseId, int lessonId)
        {
            return _context.Lessons.Where(p => p.CourseId == courseId && p.Id == lessonId).FirstOrDefault();
        }

        public IEnumerable<Lesson> GetLessonsForCourse(int courseId)
        {
            return _context.Lessons.Where(p => p.CourseId == courseId).ToList();
        }

        public void UpdateLessonInCourse(int courseId, Lesson lesson)
        {
            throw new NotImplementedException();
        }

        public void DeleteLesson(Lesson lesson)
        {
            _context.Lessons.Remove(lesson);
        }
    }
}
