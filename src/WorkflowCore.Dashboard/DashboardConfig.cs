using WorkflowCore.Dashboard.Permissions;
using WorkflowCore.Dashboard.Permissions.Managers;

namespace WorkflowCore.Dashboard;

public class DashboardConfig
{
    public string RoutePrefix { get; set; } = "/wfc-dashboard";
    public IPermissionManagerResolver PermissionManagerResolver { get; set; } = new PermissionManagerResolver<AllowLocalAccessPermissionManager>();
}
