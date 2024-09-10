using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using WorkflowCore.Interface;
using WorkflowCore.Dashboard.Api;

namespace WorkflowCore.Dashboard;

public static class DependencyInjection
{
    public static IApplicationBuilder UseWorkflowCoreDashboard(this IApplicationBuilder appBuilder, DashboardConfig? config = null)
    {
        config ??= new();

        return appBuilder.MapWhen(context => context.Request.Path.StartsWithSegments(config.RoutePrefix), app =>
        {
            app.UseRouting();
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
            });
        });
    }
}
