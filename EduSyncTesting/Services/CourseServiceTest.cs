using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using EduSyncAPI.DTOs;
using EduSyncAPI.Models;
using EduSyncAPI.Services;

namespace EduSyncAPI.Tests.Services
{
    public class CourseServiceTests : BaseTest
    {
        private CourseService _service;

        [SetUp]
        public void TestSetup()
        {
            _service = new CourseService(_context);
        }

        [Test]
        public async Task CreateCourse_ShouldAddCourse()
        {
            var instructorId = Guid.NewGuid();

            var dto = new CourseDto
            {
                Title = "Test Course",
                Description = "Description",
                MediaUrl = "http://media.url"
            };

            var course = await _service.CreateCourseAsync(dto, instructorId);

            Assert.IsNotNull(course);
            Assert.AreEqual(dto.Title, course.Title);
            Assert.AreEqual(dto.Description, course.Description);
            Assert.AreEqual(dto.MediaUrl, course.MediaUrl);
            Assert.AreEqual(instructorId, course.InstructorId);
        }

        [Test]
        public async Task GetAllCourses_ShouldReturnCourses()
        {
            _context.Courses.Add(new Course
            {
                CourseId = Guid.NewGuid(),
                Title = "Course 1",
                Description = "Desc 1",
                MediaUrl = "http://media1.url",
                InstructorId = Guid.NewGuid()
            });

            _context.Courses.Add(new Course
            {
                CourseId = Guid.NewGuid(),
                Title = "Course 2",
                Description = "Desc 2",
                MediaUrl = "http://media2.url",
                InstructorId = Guid.NewGuid()
            });

            await _context.SaveChangesAsync();

            var courses = await _service.GetAllAsync();

            Assert.AreEqual(2, courses.Count());
        }

        [Test]
        public async Task GetCourseById_ShouldReturnCourse()
        {
            var course = new Course
            {
                CourseId = Guid.NewGuid(),
                Title = "Course A",
                Description = "Description A",
                MediaUrl = "http://mediaA.url",
                InstructorId = Guid.NewGuid()
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            var result = await _service.GetByIdAsync(course.CourseId);

            Assert.IsNotNull(result);
            Assert.AreEqual(course.Title, result.Title);
            Assert.AreEqual(course.Description, result.Description);
            Assert.AreEqual(course.MediaUrl, result.MediaUrl);
            Assert.AreEqual(course.InstructorId, result.InstructorId);
        }
    }
}
