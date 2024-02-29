namespace WebForum.Auth.Domain.Models;

[Flags]
public enum UserPermissions
{
    CanPublish = 0,
    CanHideAnyPosts = 1
}