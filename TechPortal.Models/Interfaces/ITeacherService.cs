using TechPortal.Models.Models;

namespace TechPortal.Models.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherOverview>> GetTeachersAsync();
    }
}
