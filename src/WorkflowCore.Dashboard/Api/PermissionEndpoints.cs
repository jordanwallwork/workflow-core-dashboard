using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using WorkflowCore.Dashboard.Permissions;
using WorkflowCore.Dashboard.Permissions.Managers;

namespace WorkflowCore.Dashboard.Api;

public static class PermissionEndpoints
{
    public static void MapPermissionEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/permissions");

        endpoints.MapGet("/", GetAll);
    }

    static async Task<string[]> GetAll(IPermissionManager permissionManager)
    {
        var permission = await permissionManager.CurrentPermission();

        return Enum.GetValues(typeof(Permission))
            .Cast<Permission>()
            .Where(p => permission.HasFlag(p))
            .Select(x => x.ToString())
            .ToArray();
    }
}
