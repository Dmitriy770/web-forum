using MediatR;
using WebForum.Auth.Application.Interfaces;
using WebForum.Auth.Domain.Exceptions;

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
        if (await userRepository.FindByUserId(request.UserId, cancellationToken) is null)
        {
            throw new UserNotFoundException(request.UserId);
        }
        
        await userRepository.Delete(request.UserId, cancellationToken);
    }
}