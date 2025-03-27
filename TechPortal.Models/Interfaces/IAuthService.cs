using TechPortal.Models.Models;

namespace TechPortal.Models.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> SignupAsync(Teacher teacher);
        Task<Result<string>> LoginAsync(LoginRequest request);
    }
}
