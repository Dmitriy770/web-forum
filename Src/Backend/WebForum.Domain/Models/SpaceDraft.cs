﻿namespace WebForum.Domain.Models;

public class SpaceDraft
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public SpaceType Type { get; init; }
}