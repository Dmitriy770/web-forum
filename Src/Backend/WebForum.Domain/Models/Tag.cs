namespace WebForum.Domain.Models;

public record Tag
{
    public Guid Id { get; init; }
    public string Name { get; init; }
};