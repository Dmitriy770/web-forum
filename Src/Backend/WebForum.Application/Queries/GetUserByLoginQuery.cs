using MediatR;
using WebForum.Application.Interfaces;
using WebForum.Domain.Models;
using WebForum.Domain.Models.UserModels;

namespace WebForum.Application.Queries;

public record GetUserByLoginQuery(
    string Login
) : IRequest<User>;

internal sealed class GetUserByLoginQueryHandler(
    IUserRepository userRepository
) : IRequestHandler<GetUserByLoginQuery, User>
{
    public async Task<User> Handle(GetUserByLoginQuery request, CancellationToken cancellationToken)
    {
        var login = request.Login;

        var user = await userRepository.GetByLogin(login, cancellationToken);

        return user;
    }
}