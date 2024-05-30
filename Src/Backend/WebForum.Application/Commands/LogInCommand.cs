using MediatR;
using WebForum.Application.Interfaces;
using WebForum.Domain.Models.AuthModels;

namespace WebForum.Application.Commands;

public record LogInCommand(
    AuthDraft AuthDraft
) : IRequest<(Auth auth, string token)>;

internal sealed class LogInCommandHandler(
    IHasher hasher,
    IJwtProvider jwtProvider,
    IUserRepository userRepository,
    ICredentialRepository credentialRepository
) : IRequestHandler<LogInCommand, (Auth auth, string token)>
{
    public async Task<(Auth auth, string token)> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        var authDraft = request.AuthDraft;

        var user = await userRepository.GetByLogin(authDraft.Login, cancellationToken);
        var credential = await credentialRepository.GetById(user.Id, cancellationToken);

        if (!hasher.Verify(authDraft.Password, credential.PasswordHash))
        {
            throw new ArgumentException(nameof(authDraft));
        }

        var (token, accessToken) = jwtProvider.GenerateToken(credential);

        var auth = new Auth
        {
            Id = user.Id,
            User = user,
            AccessToken = accessToken
        };
        
        return (auth, token);
    }
}