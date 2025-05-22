using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using EduSyncAPI.DTOs;
using EduSyncAPI.Models;
using EduSyncAPI.Services;

namespace EduSyncAPI.Tests.Services
{
    public class AuthServiceTests : BaseTest
    {
        private AuthService _service;
        private IConfiguration _config;

        [SetUp]
        public void TestSetup()
        {
            // Generate a 256-bit (32-byte) secret key for HS256 signing
            var keyBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }
            var secretKeyBase64 = Convert.ToBase64String(keyBytes);

            var inMemorySettings = new Dictionary<string, string> {
                {"JwtSettings:SecretKey", secretKeyBase64},
                {"JwtSettings:Issuer", "TestIssuer"},
                {"JwtSettings:Audience", "TestAudience"},
                {"JwtSettings:ExpiryMinutes", "60"}
            };

            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _service = new AuthService(_context, _config);
        }

        [Test]
        public async Task RegisterUser_ShouldAddUser()
        {
            var dto = new UserDto
            {
                Name = "Auth Test User",
                Email = "authuser@example.com",
                Password = "password123",
                Role = "Student"
            };

            var user = await _service.RegisterAsync(dto);

            Assert.IsNotNull(user);
            Assert.AreEqual(dto.Email, user.Email);
            Assert.AreEqual(dto.Name, user.Name);
            Assert.AreEqual(dto.Role, user.Role);
            Assert.IsNotNull(user.PasswordHash);
        }

        [Test]
        public async Task Login_ShouldReturnToken_WhenCredentialsAreCorrect()
        {
            // First register a user to login
            var userDto = new UserDto
            {
                Name = "Login User",
                Email = "loginuser@example.com",
                Password = "password123",
                Role = "Student"
            };
            await _service.RegisterAsync(userDto);

            var loginDto = new LoginDto
            {
                Email = "loginuser@example.com",
                Password = "password123"
            };

            var response = await _service.LoginAsync(loginDto);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Token);
            Assert.AreEqual("Student", response.Role);
        }

        [Test]
        public async Task Login_ShouldReturnNull_WhenCredentialsAreWrong()
        {
            var loginDto = new LoginDto
            {
                Email = "nonexistent@example.com",
                Password = "wrongpassword"
            };

            var response = await _service.LoginAsync(loginDto);

            Assert.IsNull(response);
        }
    }
}
