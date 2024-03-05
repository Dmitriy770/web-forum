namespace WebForum.Auth.Domain.Models;

[Flags]
public enum UserPermissions
{
    CanPublish = 0,
    CanHideOwnPosts = 1,
    CanHideAnyPosts = 2
}