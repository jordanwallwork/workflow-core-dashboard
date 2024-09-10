using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace WorkflowCore.Dashboard.Api;

public static class FileEndpoints
{
    public static void MapFileEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/index", () => GetFile("index.html", "text/html"));
        app.MapGet("/index.js", () => GetFile("index.js", "text/javascript"));
        app.MapGet("/index.css", () => GetFile("index.css", "text/css"));
    }

    private static IResult GetFile(string fileName, string contentType)
    {
        var stream = typeof(FileEndpoints).Assembly.GetManifestResourceStream($"WorkflowCore.Dashboard.wwwroot.{fileName}");
        if (stream is null) return Results.NotFound();

        return Results.Stream(stream, contentType);
    }
}
