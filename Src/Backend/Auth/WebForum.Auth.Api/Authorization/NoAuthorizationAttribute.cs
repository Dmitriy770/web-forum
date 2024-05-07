namespace WebForum.Auth.Api.Authorization;

[AttributeUsage(AttributeTargets.Method)]
public class NoAuthorizationAttribute : Attribute;