using MediatR;

namespace WebForum.Auth.Application.Commands;

public record CreateUserCommand(
    string Login,
    string Password
) : IRequest<Guid>;

internal sealed class CreateUserCommandHandler(
) : IRequestHandler<CreateUserCommand, Guid>
{
    public Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}