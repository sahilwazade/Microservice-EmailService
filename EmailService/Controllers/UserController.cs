using EmailService.Application.IServices;
using EmailService.Infrastructure.Repository;
using EmailService.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("GetProductByIdAndSendEmail")]
        public async Task<IActionResult> GetUserByName(GetUserByNameCommand command)
        {
            try
            {
                var result = await _userService.GetUserByName(command);
                if (result == null || result.IsSuccess == false)
                {
                    return BadRequest(new ApiResponse<object>(false, result.Message, null));
                }
                return Ok(new ApiResponse<Employee>(true, result.Message, result.employee));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, $"Internal server error: {ex.Message}", null));
            }
        }

        //Add email related endpoints controllers here ...
    }
}
