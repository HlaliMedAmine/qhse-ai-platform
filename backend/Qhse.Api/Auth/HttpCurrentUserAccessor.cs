namespace Qhse.Api.Auth;

public sealed class HttpCurrentUserAccessor(IHttpContextAccessor httpContextAccessor) : ICurrentUserAccessor
{
    public CurrentUser User
    {
        get
        {
            var principal = httpContextAccessor.HttpContext?.User;
            var name = principal?.Identity?.IsAuthenticated == true ? principal.Identity.Name : "MVP User";
            return new CurrentUser(null, name, []);
        }
    }
}
