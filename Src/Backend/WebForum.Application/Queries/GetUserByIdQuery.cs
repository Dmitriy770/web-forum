using MediatR;
using WebForum.Application.Interfaces;
using WebForum.Domain.Models;
using WebForum.Domain.Models.UserModels;

namespace WebForum.Application.Queries;

public record GetUserByIdQuery(
    Guid UserId
) : IRequest<User>;

internal sealed class GetUserByIdQueryHandler(
    IUserRepository userRepository
) : IRequestHandler<GetUserByIdQuery, User>
{
    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        var user = await userRepository.GetById(userId, cancellationToken);

        return user;
    }
}