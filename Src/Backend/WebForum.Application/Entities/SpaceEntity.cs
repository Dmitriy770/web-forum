using WebForum.Domain.Models;
using WebForum.Domain.Models.SpaceModels;

namespace WebForum.Application.Entities;

public record SpaceEntity
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTime CreationDate { get; init; }
    public SpaceType Type { get; init; }
    public Guid AuthorId { get; init; }
}