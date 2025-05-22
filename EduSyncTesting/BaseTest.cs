using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using EduSyncAPI.Models;

namespace EduSyncAPI.Tests
{
    public class BaseTest
    {
        protected EduSyncDbContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EduSyncDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())  // Unique DB per test
                .Options;

            _context = new EduSyncDbContext(options);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}
