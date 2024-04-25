namespace WebForum.Auth.Application.Interfaces;

public interface IHasher
{
    public string Hash(string password);

    public bool Verify(string password, string hashedPassword);
}