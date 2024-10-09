using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Dashboard.Permissions.Managers;

namespace WorkflowCore.Dashboard.Permissions;

public class PermissionManagerResolver<T> : IPermissionManagerResolver where T : IPermissionManager
{
    public IPermissionManager Resolve(IServiceProvider serviceProvider) => serviceProvider.GetRequiredService<T>();
}
