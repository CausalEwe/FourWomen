using AuthorizationService.Application.Commands;
using Grpc.Core;
using MediatR;

namespace AuthorizationService.Grpc.Services;

public class AuthService(IMediator mediator) : Auth.AuthBase
{
    public override async Task<UserLoginTransfer> Login(UserLoginRequest userLoginRequest, ServerCallContext context)
    {
        if (string.IsNullOrEmpty(userLoginRequest.Username) || string.IsNullOrEmpty(userLoginRequest.Password))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Username and password are required"));
        }
        
        try
        {
            var result = await mediator.Send(
                new UserLoginCommand { Username = userLoginRequest.Username, Password = userLoginRequest.Password });
            
            return new UserLoginTransfer { Token = result.Token };
        }
        catch (UnauthorizedAccessException e)
        {   
            throw new RpcException(new Status(StatusCode.Unauthenticated, e.Message));
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, $"Произошла ошибка при обработке вашего запроса. {ex.Message}"));
        }
    }
}