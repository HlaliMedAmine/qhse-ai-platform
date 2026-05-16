namespace Qhse.Api.Auth;

public interface ICurrentUserAccessor
{
    CurrentUser User { get; }
}
