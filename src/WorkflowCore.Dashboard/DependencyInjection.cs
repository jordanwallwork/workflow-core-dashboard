using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Dashboard.Api;
using WorkflowCore.Dashboard.Permissions;
using WorkflowCore.Dashboard.Permissions.Managers;

namespace WorkflowCore.Dashboard;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkflowCoreDashboard(this IServiceCollection serviceCollection, DashboardConfig? config = null)
    {
        config ??= new();

        serviceCollection.AddTransient(sp => config ?? new());
        serviceCollection.AddTransient(config.PermissionManagerResolver.Resolve);
        serviceCollection.AddSingleton<IAuthorizationPolicyProvider, RequirePermissionPolicyProvider>();
        serviceCollection.AddSingleton<IAuthorizationHandler, RequirePermissionAuthHandler>();
        serviceCollection.AddTransient<AllowLocalAccessPermissionManager>();

        return serviceCollection;
    }

    public static IApplicationBuilder UseWorkflowCoreDashboard(this IApplicationBuilder appBuilder)
    {
        var config = appBuilder.ApplicationServices.GetRequiredService<DashboardConfig>();

        return appBuilder.MapWhen(context => context.Request.Path.StartsWithSegments(config.RoutePrefix), app =>
        {
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(x =>
            {
                var endpoints = x.MapGroup(config.RoutePrefix);

                x.MapGet(config.RoutePrefix, ctx =>
                {
                    ctx.Response.Redirect(config.RoutePrefix + "/index", permanent: true);
                    return Task.CompletedTask;
                });

                endpoints.MapFileEndpoints();
                endpoints.MapDefinitionEndpoints();
                endpoints.MapWorkflowEndpoints();
                endpoints.MapPermissionEndpoints();
            });
        });
    }
}
