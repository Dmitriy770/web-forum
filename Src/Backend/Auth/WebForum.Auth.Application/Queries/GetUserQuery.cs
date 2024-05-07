using MediatR;
using WebForum.Auth.Domain.Models;

namespace WebForum.Auth.Application.Queries;

public record GetUserQuery(
    Guid UserId
) : IRequest<User>;