using MediatR;
using WebForum.Auth.Application.Interfaces;
using WebForum.Auth.Domain.Models;

namespace WebForum.Auth.Application.Queries;

public record GetUserByAccessTokenQuery(
    string AccessToken
) : IRequest<User?>;

internal sealed class GetUserByAccessTokenQueryHandler(
    IUserRepository userRepository,
    IJwtProvider jwtProvider
) : IRequestHandler<GetUserByAccessTokenQuery, User?>
{
    public async Task<User?> Handle(GetUserByAccessTokenQuery request, CancellationToken cancellationToken)
    {
        var accessToken = request.AccessToken;

        var (isValid, userId) = await jwtProvider.ValidateToken(accessToken);
        if (!isValid)
        {
            return null;
        }

        return await userRepository.FindByUserId(userId, cancellationToken);
    }
}