using System.Text.Json.Serialization;

namespace WebForum.Frontend.Models;

[Flags]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserPermissions
{
    CanPublish = 1,
    CanHideOwnPosts = 2,
    CanHideAnyPosts = 4
}