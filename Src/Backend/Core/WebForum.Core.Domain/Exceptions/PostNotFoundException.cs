namespace WebForum.Core.Domain.Exceptions;

public class PostNotFoundException(Guid Id) : Exception($"Post with id {Id} not found");