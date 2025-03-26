using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TeachPortal.DataStore;
using TechPortal.Models.Interfaces;
using TechPortal.Models.Models;

namespace TeachPortal.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthService> _logger;

        public AuthService(AppDbContext dbContext, IConfiguration config, ILogger<AuthService> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<string>> SignupAsync(Teacher teacher)
        {
            try
            {
                if (teacher == null)
                {
                    _logger.LogError("Invalid teacher data: teacher is null");
                    return new Result<string>(false, "Invalid teacher data");
                }

                _logger.LogInformation("Registering new teacher: {UserName}, {Email}", teacher.UserName, teacher.Email);

                teacher.PasswordHash = BCrypt.Net.BCrypt.HashPassword(teacher.PasswordHash);

                await _dbContext.Teachers.AddAsync(teacher);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Teacher registered successfully: {UserName}, {Email}", teacher.UserName, teacher.Email);

                return new Result<string>(true, "Teacher registered successfully");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database update error occurred while registering teacher: {UserName}, {Email}", teacher?.UserName, teacher?.Email);
                return new Result<string>(false, "Database update error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering teacher: {UserName}, {Email}", teacher?.UserName, teacher?.Email);
                return new Result<string>(false, "An error occurred while registering the teacher");
            }
        }

        public async Task<Result<string>> LoginAsync(LoginRequest request)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogError("Invalid login request: request is null");
                    return new Result<string>(false, "Invalid login request");
                }

                _logger.LogInformation("Logging in teacher: {UserName}", request.Username);

                var teacher = await _dbContext.Teachers.FirstOrDefaultAsync(t => t.UserName == request.Username);
                if (teacher == null || !BCrypt.Net.BCrypt.Verify(request.Password, teacher.PasswordHash))
                {
                    _logger.LogError("Teacher not found or invalid password: {UserName}", request.Username);
                    return new Result<string>(false, "Invalid username or password");
                }

                var token = GenerateJwtToken(teacher);

                _logger.LogInformation("Teacher logged in successfully: {UserName}", request.Username);
                return new Result<string>(true, "Teacher logged in successfully", token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging in teacher: {UserName}", request.Username);
                return new Result<string>(false, "An error occurred while logging in the teacher");
            }
        }

        private string GenerateJwtToken(Teacher teacher)
        {
            var claims = new[]
            {
                    new Claim(ClaimTypes.Name, teacher.UserName),
                    new Claim(ClaimTypes.Email, teacher.Email),
                    new Claim(ClaimTypes.NameIdentifier, teacher.Id.ToString())
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
