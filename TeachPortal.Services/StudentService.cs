using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TeachPortal.DataStore;
using TechPortal.Models.Interfaces;
using TechPortal.Models.Models;


namespace TeachPortal.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<StudentService> _logger;

        public StudentService(AppDbContext dbContext, ILogger<StudentService> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<StudentResult> CreateStudentAsync(Student student, int teacherId)
        {
            try
            {
              

                if (student == null)
                {
                   
                    return new StudentResult { Success = false, Message = "Invalid student data" };
                }

                var teacher = await _dbContext.Teachers.FindAsync(teacherId);
                if (teacher == null)
                {
                    
                    return new StudentResult { Success = false, Message = "Teacher not found" };
                }

                student.Teacher = teacher;
                await _dbContext.Students.AddAsync(student);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Student registered successfully: {StudentId}", student.Id);
                return new StudentResult { Success = true, Message = "Student registered successfully", Student = student };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database update error occurred while registering student: {StudentId}", student?.Id);
                return new StudentResult { Success = false, Message = "Database update error" };
            }
        }

        public async Task<IEnumerable<Student>> GetStudentsByTeacherAsync(int teacherId)
        {
            return await _dbContext.Students
                .Where(s => s.Teacher.Id == teacherId)
                .ToListAsync();
        }
    }
}
