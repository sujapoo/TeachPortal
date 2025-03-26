using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechPortal.Models.Models;

namespace TechPortal.Models.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherOverview>> GetTeachersAsync();
    }
}
