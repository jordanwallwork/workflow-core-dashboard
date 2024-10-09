using WorkflowCore.Dashboard.Permissions.Managers;

namespace WorkflowCore.Dashboard.Permissions;

public interface IPermissionManagerResolver
{
    IPermissionManager Resolve(IServiceProvider serviceProvider);
}
