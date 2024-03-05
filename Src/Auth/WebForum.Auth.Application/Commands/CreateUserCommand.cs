using MediatR;
using WebForum.Auth.Application.Interfaces;
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
        var userId = Guid.NewGuid();
        var hashedPassword = hasher.Hash(request.Password);

        var user = User.Create(userId, request.Login, hashedPassword, UserPermissions.CanPublish);
        await userRepository.Save(user, cancellationToken);

        return userId;
    }
}