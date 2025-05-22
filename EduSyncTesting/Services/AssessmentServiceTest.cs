using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using EduSyncAPI.DTOs;
using EduSyncAPI.Models;
using EduSyncAPI.Services;

namespace EduSyncAPI.Tests.Services
{
    public class AssessmentServiceTests : BaseTest
    {
        private AssessmentService _service;

        [SetUp]
        public void TestSetup()
        {
            _service = new AssessmentService(_context);
        }

        [Test]
        public async Task CreateAssessment_ShouldAddAssessment()
        {
            var dto = new AssessmentDto
            {
                CourseId = Guid.NewGuid(),
                Title = "Test Assessment",
                Questions = "Q1,Q2,Q3",
                MaxScore = 100
            };

            var assessment = await _service.CreateAsync(dto);

            Assert.IsNotNull(assessment);
            Assert.AreEqual(dto.Title, assessment.Title);
            Assert.AreEqual(dto.MaxScore, assessment.MaxScore);
            Assert.AreEqual(dto.Questions, assessment.Questions);
            Assert.AreEqual(dto.CourseId, assessment.CourseId);
        }

        [Test]
        public async Task GetAllAssessments_ShouldReturnAssessments()
        {
            _context.Assessments.Add(new Assessment
            {
                AssessmentId = Guid.NewGuid(),
                Title = "Assessment 1",
                Questions = "Q1,Q2",
                MaxScore = 50,
                CourseId = Guid.NewGuid()
            });
            _context.Assessments.Add(new Assessment
            {
                AssessmentId = Guid.NewGuid(),
                Title = "Assessment 2",
                Questions = "Q3,Q4",
                MaxScore = 75,
                CourseId = Guid.NewGuid()
            });
            await _context.SaveChangesAsync();

            var assessments = await _service.GetAllAsync();

            Assert.AreEqual(2, assessments.Count());
        }

        [Test]
        public async Task GetAssessmentById_ShouldReturnAssessment()
        {
            var assessment = new Assessment
            {
                AssessmentId = Guid.NewGuid(),
                Title = "Assessment A",
                Questions = "Q1,Q2,Q3",
                MaxScore = 100,
                CourseId = Guid.NewGuid()
            };
            _context.Assessments.Add(assessment);
            await _context.SaveChangesAsync();

            var result = await _service.GetByIdAsync(assessment.AssessmentId);

            Assert.IsNotNull(result);
            Assert.AreEqual(assessment.Title, result.Title);
            Assert.AreEqual(assessment.Questions, result.Questions);
            Assert.AreEqual(assessment.MaxScore, result.MaxScore);
            Assert.AreEqual(assessment.CourseId, result.CourseId);
        }
    }
}
