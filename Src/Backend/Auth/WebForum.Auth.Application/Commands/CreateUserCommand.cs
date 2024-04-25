using MediatR;
using WebForum.Auth.Application.Interfaces;
using WebForum.Auth.Domain.Exceptions;
using WebForum.Auth.Domain.Models;

namespace WebForum.Auth.Application.Commands;

public record CreateUserCommand(
    string Login,
    string Password
) : IRequest<Guid>;

internal sealed class CreateUserCommandHandler(
    IUserRepository userRepository,
    IHasher hasher
) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.FindByLogin(request.Login, cancellationToken) is not null)
        {
            throw new UserAlreadyExistsException(request.Login);
        }
        
        var userId = Guid.NewGuid();
        var hashedPassword = hasher.Hash(request.Password);

        var user = User.Create(userId, request.Login, hashedPassword, UserPermissions.CanPublish | UserPermissions.CanHideAnyPosts);
        await userRepository.Save(user, cancellationToken);

        return userId;
    }
}