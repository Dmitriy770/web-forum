using MediatR;
using WebForum.Auth.Application.Interfaces;
using WebForum.Auth.Domain.Exceptions;

namespace WebForum.Auth.Application.Queries;

public record GetAccessTokenQuery(
    string Login,
    string Password
) : IRequest<string>;


internal sealed class GetAccessTokenQueryHandler(
    IUserRepository userRepository,
    IHasher hasher,
    IJwtProvider jwtProvider
) : IRequestHandler<GetAccessTokenQuery, string>
{
    public async Task<string> Handle(GetAccessTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByLogin(request.Login, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.Login);
        }

        if (!hasher.Verify(request.Password, user.HashedPassword))
        {
            throw new InvalidPasswordException();
        }

        return jwtProvider.GenerateToken(user);
    }
}