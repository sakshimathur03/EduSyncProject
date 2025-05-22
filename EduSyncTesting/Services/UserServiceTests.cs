using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using EduSyncAPI.DTOs;
using EduSyncAPI.Models;
using EduSyncAPI.Services;

namespace EduSyncAPI.Tests.Services
{
    public class UserServiceTests : BaseTest
    {
        private UserService _service;

        [SetUp]
        public void TestSetup()
        {
            _service = new UserService(_context);
        }

        [Test]
        public async Task CreateUser_ShouldAddUser()
        {
            var dto = new UserDto
            {
                Name = "Test User",
                Email = "testuser@example.com",
                Password = "password",
                Role = "Student"
            };

            var user = await _service.CreateAsync(dto);

            Assert.IsNotNull(user);
            Assert.AreEqual(dto.Email, user.Email);
            Assert.AreEqual(dto.Name, user.Name);
            Assert.AreEqual(dto.Role, user.Role);
        }

        [Test]
        public async Task GetAllUsers_ShouldReturnUsers()
        {
            _context.Users.Add(new User
            {
                UserId = Guid.NewGuid(),
                Email = "a@a.com",
                Name = "User A",
                PasswordHash = "hashedpwdA",
                Role = "Student"
            });
            _context.Users.Add(new User
            {
                UserId = Guid.NewGuid(),
                Email = "b@b.com",
                Name = "User B",
                PasswordHash = "hashedpwdB",
                Role = "Instructor"
            });
            await _context.SaveChangesAsync();

            var users = await _service.GetAllAsync();

            Assert.AreEqual(2, users.Count());
        }

        [Test]
        public async Task GetUserById_ShouldReturnUser()
        {
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Email = "abc@example.com",
                Name = "ABC User",
                PasswordHash = "hashedpwdABC",
                Role = "Admin"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = await _service.GetByIdAsync(user.UserId);

            Assert.IsNotNull(result);
            Assert.AreEqual("abc@example.com", result.Email);
            Assert.AreEqual("ABC User", result.Name);
            Assert.AreEqual("Admin", result.Role);
        }
    }
}
