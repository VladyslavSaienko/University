using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using University.API.Entities;
using University.API.Interfaces;
using University.API.Models;

namespace University.API.Controllers
{
    [ApiController]
    [Route("api/courses/{cityId}/lessons")]
    public class LessonsController : ControllerBase
    {
        private readonly ILogger<LessonsController> _logger;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public LessonsController(ILogger<LessonsController> logger, ICourseRepository courseRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetLessons(int courseId)
        {
            try
            {
                var course = _courseRepository.Get(courseId, true);
                if (course == null)
                {
                    _logger.LogInformation($"Course with id {courseId} wasn`t found");
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<LessonDto>>(course.Lessons));
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting course by  id {courseId}", ex);
                return StatusCode(500, "An exception happened while processing request");
            }
        }

        [HttpGet("{id}", Name = "GetLesson")]
        public IActionResult GetLesson(int courseId, int id)
        {
            var course = _courseRepository.Get(courseId, false);

            if (course == null)
            {
                return NotFound();
            }

            var lesson = _courseRepository.GetLesson(courseId, id);

            if (lesson == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LessonDto>(lesson));
        }

        [HttpPost]
        public IActionResult CreateLesson(int courseId, [FromBody] CreateLessonDto lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var course = _courseRepository.Get(courseId, true);
            if (course == null)
            {
                return NotFound();
            }

            var lessonToSave = _mapper.Map<Lesson>(lesson);

            _courseRepository.AddLessonToCourse(courseId, lessonToSave);
            _courseRepository.Save();

            var createdLesson = _mapper.Map<LessonDto>(lessonToSave);

            return CreatedAtRoute("GetLesson", new { courseId, id = createdLesson.Id },
                createdLesson);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateLesson(int courseId, int id, [FromBody] UpdateLessonDto lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var course = _courseRepository.Get(courseId, true);
            if (course == null)
            {
                return NotFound();
            }
            var existedLesson = course.Lessons.FirstOrDefault(c => c.Id == id);
            if (existedLesson == null)
            {
                return NotFound();
            }

            _mapper.Map(lesson, existedLesson);

            _courseRepository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUpdateLesson(int courseId, int id, [FromBody] JsonPatchDocument<UpdateLessonDto> patchDoc)
        {

            var course = _courseRepository.Get(courseId, true);
            if (course == null)
            {
                return NotFound();
            }
            var existedLesson = course.Lessons.FirstOrDefault(c => c.Id == id);
            if (existedLesson == null)
            {
                return NotFound();
            }

            var lessonToPatch = _mapper.Map<UpdateLessonDto>(existedLesson);

            patchDoc.ApplyTo(lessonToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _courseRepository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLesson(int courseId, int id)
        {
            var course = _courseRepository.Get(courseId, true);
            if (course == null)
            {
                return NotFound();
            }
            var existedLesson = course.Lessons.FirstOrDefault(c => c.Id == id);
            if (existedLesson == null)
            {
                return NotFound();
            }

            _courseRepository.DeleteLesson(existedLesson);
            _courseRepository.Save();

            return NoContent();
        }
    }
}
