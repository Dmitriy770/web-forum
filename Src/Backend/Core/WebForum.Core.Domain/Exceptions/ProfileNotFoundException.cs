namespace WebForum.Core.Domain.Exceptions;

public class ProfileNotFoundException(Guid userId) : Exception($"Profile with userId {userId} not found");