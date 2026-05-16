namespace Qhse.Api.Auth;

public sealed record CurrentUser(Guid? UserId, string? DisplayName, string[] Roles);
