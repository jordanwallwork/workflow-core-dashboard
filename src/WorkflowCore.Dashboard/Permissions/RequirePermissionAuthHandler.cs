using Microsoft.AspNetCore.Authorization;
using WorkflowCore.Dashboard.Permissions.Managers;

namespace WorkflowCore.Dashboard.Permissions;

internal class RequirePermissionAuthHandler :  AuthorizationHandler<RequirePermissionRequirement>
{
    private readonly IPermissionManager _permissionManager;

    public RequirePermissionAuthHandler(IPermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RequirePermissionRequirement requirement)
    {
        if (await HasPermission(requirement.Permission))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail(new(this, "Permission required: " + requirement.Permission.ToString()));
        }
    }

    private async Task<bool> HasPermission(Permission permission)
    {
        var currentPermission = await _permissionManager.CurrentPermission();
        return currentPermission.HasFlag(permission);
    }
}