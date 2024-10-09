using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using WorkflowCore.Dashboard.Dtos;
using WorkflowCore.Dashboard.Permissions;
using WorkflowCore.Interface;

namespace WorkflowCore.Dashboard.Api;

public static class DefinitionEndpoints
{
    public static void MapDefinitionEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/definitions").RequirePermission(Permission.ViewDefinitions);

        endpoints.MapGet("/", GetAll);
    }

    static IEnumerable<DefinitionDto> GetAll(IWorkflowRegistry registry)
    {
        return registry.GetAllDefinitions().Select(x => new DefinitionDto(x));
    }
}
