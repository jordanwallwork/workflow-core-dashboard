using Microsoft.AspNetCore.Http;

namespace WorkflowCore.Dashboard.Permissions.Managers;

public class AllowLocalAccessPermissionManager : IPermissionManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AllowLocalAccessPermissionManager(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<Permission> CurrentPermission() => Task.FromResult(IsLocalRequest() ? Permission.All : Permission.None);

    private bool IsLocalRequest()
    {
        var connection = _httpContextAccessor.HttpContext?.Connection;
        return connection?.LocalIpAddress?.Equals(connection.RemoteIpAddress) == true;
    }
}
