using AuthorizationService.Application.Commands;
using AuthorizationService.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register()
    {
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest userLoginRequest)
    {
        try
        {
            var result = await mediator.Send(
                new UserLoginCommand { Username = userLoginRequest.Username, Password = userLoginRequest.Password });
            
            return Ok(result);
        }
        catch (UnauthorizedAccessException e)
        {   
            return Unauthorized(e.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Произошла ошибка при обработке вашего запроса. {ex.Message}");
        }
    }
}