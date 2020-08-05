using System.Collections.Generic;
using University.API.Entities;

namespace University.API.Interfaces
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAll();
        Course Get(int id, bool includeLessons);

        IEnumerable<Lesson> GetLessonsForCourse(int courseId);
        Lesson GetLesson(int courseId, int lessonId);
        void AddLessonToCourse(int courseId, Lesson lesson);
        void UpdateLessonInCourse(int courseId, Lesson lesson);
        void DeleteLesson(Lesson lesson);

        bool Save();
    }
}
