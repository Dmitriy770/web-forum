using MediatR;
using WebForum.Application.Interfaces;
using WebForum.Domain.Models.UserModels;

namespace WebForum.Application.Commands;

public record ValidateLogInCommand(
    string AccessToken
) : IRequest<User>;

internal sealed class ValidateLogInCommandHandler(
    IJwtProvider jwtProvider,
    IUserRepository userRepository
    ) : IRequestHandler<ValidateLogInCommand, User>
{
    public async Task<User> Handle(ValidateLogInCommand request, CancellationToken cancellationToken)
    {
        var accessToken = request.AccessToken;

        var userId = await jwtProvider.ValidateToken(accessToken);
        if (userId is null)
        {
            throw new ArgumentException(nameof(accessToken));
        }

        var user = await userRepository.GetById(userId.Value, cancellationToken);
        
        return user;
    }
}