using MediatR;
using WebForum.Auth.Application.Interfaces;

namespace WebForum.Auth.Application.Commands;

public record DeleteUserCommand(
    Guid UserId
) : IRequest;

internal sealed class DeleteUserCommandHandler(
    IUserRepository userRepository
) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await userRepository.Delete(request.UserId, cancellationToken);
    }
}