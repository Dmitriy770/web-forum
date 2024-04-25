namespace WebForum.Auth.Domain.Models;

[Flags]
public enum UserPermissions
{
    CanPublish = 1,
    CanHideOwnPosts = 2,
    CanHideAnyPosts = 4
}