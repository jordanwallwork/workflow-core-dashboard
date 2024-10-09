using Microsoft.Extensions.DependencyInjection;

namespace WorkflowCore.Dashboard;

public class DashboardConfig
{
    public string RoutePrefix { get; set; } = "/wfc-dashboard";

    public Type? PermissionManager { get; set; }
    public ServiceLifetime? PermissionManagerLifetime { get; set; }
}
