﻿using Microsoft.AspNetCore.Mvc;
using TechPortal.Models.Interfaces;
using TechPortal.Models.Models;

namespace TeachPortal.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new teacher.
        /// </summary>
        /// <param name="teacher">The teacher to register.</param>
        /// <returns>A response indicating the result of the registration.</returns>
        /// <response code="200">Teacher registered successfully.</response>
        /// <response code="400">Invalid teacher data.</response>
        /// <response code="403">Database update error occurred while registering teacher.</response>
        [HttpPost("signup")]        
        public async Task<IActionResult> SignupAsync([FromBody] Teacher teacher)
        {
            try { 
            var result = await _authService.SignupAsync(teacher);
            if (result == null || !result.Success)
            {
                _logger.LogError("Error during signup: {Message}", result?.Message);
                return StatusCode(403, result?.Message);
            }
            return Ok(result.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering teacher");
                return StatusCode(403, "An error occurred while registering teacher");
            }
        }

        /// <summary>
        /// Logs in a teacher.
        /// </summary>
        /// <param name="request">The login request containing username and password.</param>
        /// <returns>A response indicating the result of the login.</returns>
        /// <response code="200">Teacher logged in successfully.</response>
        /// <response code="400">Invalid login request.</response>
        /// <response code="403">Error occurred while logging in.</response>
        [HttpPost("login")]        
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            try { 
            var result = await _authService.LoginAsync(request);
            if (result == null || !result.Success)
            {
                _logger.LogError("Error during login: {Message}", result?.Message);
                return StatusCode(403, result?.Message);
            }
            return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging in");
                return StatusCode(403, "An error occurred while logging in");
            }
        }
    }
}
