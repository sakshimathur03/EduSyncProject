using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using EduSyncAPI.DTOs;
using EduSyncAPI.Models;
using EduSyncAPI.Services;

namespace EduSyncAPI.Tests.Services
{
    public class ResultServiceTests : BaseTest
    {
        private ResultService _service;

        [SetUp]
        public void TestSetup()
        {
            _service = new ResultService(_context);
        }

        [Test]
        public async Task CreateResult_ShouldAddResult()
        {
            var dto = new ResultDto
            {
                AssessmentId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Score = 85
            };

            var result = await _service.CreateAsync(dto);

            Assert.IsNotNull(result);
            Assert.AreEqual(dto.Score, result.Score);
            Assert.AreEqual(dto.AssessmentId, result.AssessmentId);
            Assert.AreEqual(dto.UserId, result.UserId);
        }

        [Test]
        public async Task GetAllResults_ShouldReturnResults()
        {
            _context.Results.Add(new Result
            {
                ResultId = Guid.NewGuid(),
                Score = 70,
                AssessmentId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            });

            _context.Results.Add(new Result
            {
                ResultId = Guid.NewGuid(),
                Score = 90,
                AssessmentId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            });

            await _context.SaveChangesAsync();

            var results = await _service.GetAllAsync();

            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public async Task GetResultById_ShouldReturnResult()
        {
            var result = new Result
            {
                ResultId = Guid.NewGuid(),
                Score = 75,
                AssessmentId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            var found = await _service.GetByIdAsync(result.ResultId);

            Assert.IsNotNull(found);
            Assert.AreEqual(result.Score, found.Score);
            Assert.AreEqual(result.AssessmentId, found.AssessmentId);
            Assert.AreEqual(result.UserId, found.UserId);
        }
    }
}
