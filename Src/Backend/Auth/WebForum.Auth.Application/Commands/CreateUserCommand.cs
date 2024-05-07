using Common.Guards;
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
        var (login, password) = request;
        
        if (await userRepository.FindByLogin(login, cancellationToken) is not null)
        {
            throw new UserAlreadyExistsException(login);
        }
        
        var userId = Guid.NewGuid();
        var hashedPassword = hasher.Hash(password);

        var user = User.Create(userId, login, hashedPassword, Permissions.CanPublish);
        await userRepository.Save(user, cancellationToken);

        return user.Id;
    }
}