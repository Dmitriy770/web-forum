using MediatR;
using WebForum.Application.Interfaces;
using WebForum.Domain.Models;
using WebForum.Domain.Models.UserModels;

namespace WebForum.Application.Commands;

public record CreateUserCommand(
    UserDraft UserDraft
) : IRequest<User>;

internal sealed class CreateUserCommandHandler(
    IUserRepository userRepository,
    ICredentialRepository credentialRepository,
    IHasher hasher
    ) : IRequestHandler<CreateUserCommand, User>
{
    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userDraft = request.UserDraft;

        var userId = Guid.NewGuid();
        
        var avatarUri = userDraft.AvatarUri ?? new Uri("/");
        var name = userDraft.Name ?? userDraft.Login;
        var user = new User
        {
            Id = userId,
            Login = userDraft.Login,
            Name = name,
            AvatarUri = avatarUri
        };

        var passwordHash = hasher.Hash(userDraft.Password);
        var credential = new Credential
        {
            Id = userId,
            Login = userDraft.Login,
            PasswordHash = passwordHash
        };

        await userRepository.Add(user, cancellationToken);
        await credentialRepository.Add(credential, cancellationToken);

        return user;
    }
}