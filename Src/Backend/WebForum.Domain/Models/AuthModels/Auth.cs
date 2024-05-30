using WebForum.Domain.Models.UserModels;

namespace WebForum.Domain.Models.AuthModels;

public record Auth
{
    public Guid Id { get; set; }
    public AccessToken AccessToken { get; set; }
    public User User { get; set; }
}