using Microsoft.AspNetCore.Authorization;

namespace WorkflowCore.Dashboard.Permissions;

internal class RequirePermissionRequirement : IAuthorizationRequirement
{
    public Permission Permission { get; set; }
}
