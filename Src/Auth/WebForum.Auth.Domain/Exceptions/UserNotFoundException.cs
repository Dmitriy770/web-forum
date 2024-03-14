namespace WebForum.Auth.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(Guid id) : base($"User with {id} not found")
    {
    }

    public UserNotFoundException(string login) : base($"User with {login} not found")
    {
    }
}