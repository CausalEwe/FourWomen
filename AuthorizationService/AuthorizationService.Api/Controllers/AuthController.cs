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
    public async Task<IActionResult> Register([FromBody]UserRegisterRequest registerRequest)
    {
        try
        {
            var result = await mediator.Send(
                new UserRegisterCommand
                {
                    Username = registerRequest.Username, Password = registerRequest.Password,
                    Email = registerRequest.Email
                });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return ex switch
            {
                ArgumentException argumentEx => BadRequest(argumentEx.Message),
                InvalidOperationException invalidOperationEx => BadRequest(invalidOperationEx.Message),
                UnauthorizedAccessException unauthorizedAccessEx => Unauthorized(unauthorizedAccessEx.Message),
                _ => StatusCode(500, $"Произошла ошибка при обработке вашего запроса. {ex.Message}")
            };
        }
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

    [HttpPost("confirm")]
    public async Task<IActionResult> Confirm(UserConfirmRequest userConfirmRequest)
    {
        try
        {
            var result = await mediator.Send(
                new UserConfirmCommand { ConfirmationToken = userConfirmRequest.ConfirmationToken });
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Произошла ошибка при обработке вашего запроса. {ex.Message}");
        }
    }
}