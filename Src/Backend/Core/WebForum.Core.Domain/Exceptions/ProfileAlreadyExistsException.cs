namespace WebForum.Core.Domain.Exceptions;

public class ProfileAlreadyExistsException(Guid userId) : Exception($"Profile with userId {userId} already exists");