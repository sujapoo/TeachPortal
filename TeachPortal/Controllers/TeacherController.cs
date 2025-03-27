using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechPortal.Models.Interfaces;
using TechPortal.Models.Models;

namespace TeachPortal.Controllers
{


    [Route("api/teacher")]
    [ApiController]
    [Authorize]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(ITeacherService teacherService, ILogger<TeacherController> logger)
        {
            _teacherService = teacherService ?? throw new ArgumentNullException(nameof(teacherService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherOverview>>> GetTeachersAsync()
        {
            try
            {
                var teachers = await _teacherService.GetTeachersAsync();
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving teachers");
                return StatusCode(403, "An error occurred while retrieving teachers");
            }
        }
    }

}
