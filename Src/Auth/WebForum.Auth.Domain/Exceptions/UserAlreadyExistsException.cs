namespace WebForum.Auth.Domain.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string login) : base($"User with login {login} already exists")
    {
    }

    public UserAlreadyExistsException(Guid id) : base($"User with id {id} already exists")
    {
    }
}