using Microsoft.AspNetCore.Builder;

namespace WorkflowCore.Dashboard.Permissions;

internal static class AuthorizationExtensions
{
    internal static TBuilder RequirePermission<TBuilder>(this TBuilder builder, Permission permission) where TBuilder : IEndpointConventionBuilder
    {
        var policyName = RequirePermissionRequirementMapper.ToPolicyName(new RequirePermissionRequirement() { Permission = permission });
        return builder.RequireAuthorization(policyName);
    }
}
