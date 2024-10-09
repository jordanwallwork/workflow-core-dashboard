using System.Diagnostics.CodeAnalysis;

namespace WorkflowCore.Dashboard.Permissions;

internal static class RequirePermissionRequirementMapper
{
    const string _policyPrefix = "RequirePermission_";

    public static string ToPolicyName(this RequirePermissionRequirement requirement) => _policyPrefix + requirement.Permission.ToString();

    public static bool TryParsePolicyName(this string policyName, [NotNullWhen(true)]out RequirePermissionRequirement? requirement)
    {
        if (policyName.StartsWith(_policyPrefix, StringComparison.OrdinalIgnoreCase) && Enum.TryParse<Permission>(policyName.AsSpan(_policyPrefix.Length), out var permission))
        {
            requirement = new RequirePermissionRequirement { Permission = permission };
            return true;
        }

        requirement = null;
        return false;
    }
}