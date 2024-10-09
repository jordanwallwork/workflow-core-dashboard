using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WorkflowCore.Dashboard.Dtos;
using WorkflowCore.Dashboard.Filters;
using WorkflowCore.Dashboard.Permissions;
using WorkflowCore.Interface;

namespace WorkflowCore.Dashboard.Api;

public static class WorkflowEndpoints
{
    const int PageSize = 30;

    public static void MapWorkflowEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/workflows/{definitionId}").RequirePermission(Permission.ViewWorkflowInstances);

        endpoints.MapGet("/", Search);
        endpoints.MapGet("/{instanceId}", Get);
    }

    static async Task<SearchResults<WorkflowDto>> Search(string definitionId, [AsParameters] WorkflowFilters filters, IPersistenceProvider searchIndex)
    {
        filters ??= new();

        var results = await searchIndex.GetWorkflowInstances(filters.Status, definitionId, filters.CreatedFrom, filters.CreatedTo, (filters.Page - 1) * PageSize, PageSize);

        return new SearchResults<WorkflowDto>(filters.Page, results.Select(x => new WorkflowDto(x)));
    }

    static async Task<IResult> Get(string definitionId, string instanceId, IPersistenceProvider x)
    {
        var workflow = await x.GetWorkflowInstance(instanceId);

        if (workflow?.WorkflowDefinitionId != definitionId) return Results.NotFound();

        return Results.Ok(new WorkflowDetailDto(workflow));
    }
}
