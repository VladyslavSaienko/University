using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.API.Models
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfLessons
        {
            get
            {
                return Lessons.Count;
            }
        }

        public ICollection<LessonDto> Lessons { get; set; } = new List<LessonDto>();
    }
}
