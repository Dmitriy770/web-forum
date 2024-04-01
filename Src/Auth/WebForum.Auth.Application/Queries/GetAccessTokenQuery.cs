using System.Security.Authentication;
using MediatR;
using WebForum.Auth.Application.Interfaces;
using WebForum.Auth.Domain.Models;

namespace WebForum.Auth.Application.Queries;

public record GetAccessTokenQuery(
    string Login,
    string Password
) : IRequest<GetAccessTokenResult>;

public record GetAccessTokenResult(
    string Token,
    AuthInfo AuthInfo
);

internal sealed class GetAccessTokenQueryHandler(
    IUserRepository userRepository,
    IHasher hasher,
    IJwtProvider jwtProvider
) : IRequestHandler<GetAccessTokenQuery, GetAccessTokenResult>
{
    public async Task<GetAccessTokenResult> Handle(GetAccessTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByLogin(request.Login, cancellationToken);
        if (user is null)
        {
            throw new InvalidCredentialException();
        }

        if (!hasher.Verify(request.Password, user.HashedPassword))
        {
            throw new InvalidCredentialException();
        }

        var (token, authInfo) = jwtProvider.GenerateToken(user);
        return new GetAccessTokenResult(token, authInfo);
    }
}