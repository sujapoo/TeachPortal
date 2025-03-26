using System.Collections.Generic;
using System.Threading.Tasks;
using TechPortal.Models.Models;

namespace TechPortal.Models.Interfaces
{
    public interface IStudentService
    {
        /// <summary>
        /// Creates a new student for a specific teacher.
        /// </summary>
        /// <param name="student">The student to create.</param>
        /// <param name="teacherId">The ID of the teacher.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the creation result.</returns>
        Task<StudentResult> CreateStudentAsync(Student student, int teacherId);

        /// <summary>
        /// Retrieves students by teacher ID.
        /// </summary>
        /// <param name="teacherId">The ID of the teacher.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of students.</returns>
        Task<IEnumerable<Student>> GetStudentsByTeacherAsync(int teacherId);               
        
    }

    public class StudentResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public Student? Student { get; set; }
    }
}
