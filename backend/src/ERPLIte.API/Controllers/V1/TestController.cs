using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using ERPLite.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;



namespace ERPLite.API.Controllers.V1;

    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class TestController : ControllerBase
    {
        private readonly ICurrentUserService _currentUser;

        public TestController(
            ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                _currentUser.UserId,
                _currentUser.Email,
                _currentUser.UserName,
                _currentUser.IsAuthenticated
            });
        }
    }

