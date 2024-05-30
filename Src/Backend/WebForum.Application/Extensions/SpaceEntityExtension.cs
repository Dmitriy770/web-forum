using WebForum.Application.Entities;
using WebForum.Domain.Models;
using WebForum.Domain.Models.SpaceModels;

namespace WebForum.Application.Extensions;

public static class SpaceEntityExtension
{
    public static SpaceEntity ToEntity(this Space space)
    {
        return new SpaceEntity
        {
            Id = space.Id,
            Type = space.Type,
            Name = space.Name,
            Description = space.Description,
            CreationDate = space.CreationDate,
            AuthorId = space.Author.Id
        };
    }
}