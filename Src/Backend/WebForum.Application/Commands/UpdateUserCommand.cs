using MediatR;
using Microsoft.AspNetCore.OData.Deltas;
using WebForum.Application.Interfaces;
using WebForum.Domain.Models;
using WebForum.Domain.Models.UserModels;

namespace WebForum.Application.Commands;

public record UpdateUserCommand(
    Guid UserId,
    Delta<User> Delta
) : IRequest<User>;

internal sealed class UpdateUserCommandHandler(
    IUserRepository userRepository
) : IRequestHandler<UpdateUserCommand, User>
{
    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var (userId, delta) = request;

        var user = await userRepository.GetById(userId, cancellationToken);
        var updatedUser = delta.Patch(user);
        await userRepository.Update(updatedUser, cancellationToken);

        return updatedUser;
    }
}