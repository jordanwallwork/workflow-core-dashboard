# Workflow Core Dashboard

Simple dashboard to visualise [Workflow Core](https://github.com/danielgerlag/workflow-core) state

**Note:** This is in early development, see the [roadmap](#todo) below 

## Installation

Install the NuGet package "[WorkflowCore.Dashboard](https://www.nuget.org/packages/WorkflowCore.Dashboard)"

Using NuGet

	PM> Install-Package WorkflowCore.Dashboard
	
Using .net cli

	dotnet add package WorkflowCore.Dashboard

## Usage

Add the dashboard to your application by adding `AddWorkflowCoreDashboard()` and `UseWorkflowCoreDashboard()`:

```csharp

public void ConfigureServices(IServiceCollection services)
{
	...
    
    // Register with DI container
    services.AddWorkflowCoreDashboard(/*optionally pass in DashboardConfig here*/);
    
    // if using a custom IPermissionManager, ensure it is also registered
    // services.AddTransient<CustomPermissionResolver>();
    
    ...
}

public void Configure(IApplicationBuilder app, IWorkflowHost workflowHost)
{
    ...

    // Hook up Workflow Core + register workflows
    workflowHost.RegisterWorkflow<HelloWorldWorkflow>();
    workflowHost.Start();

    // Add dashboard
    app.UseWorkflowCoreDashboard();

    ...
}
```

By default you can access the dashboard at `/wfc-dashboard` (this path is configurable, see below)

## Configuration

You can pass a `DashboardConfig` into the `UseWorkflowCoreDashboard()`

| Property                  | Default Value                     | Notes                                                                 |
|---------------------------|-----------------------------------|-----------------------------------------------------------------------|
| RoutePrefix               | /wfc-dashboard                    | Change this if you want to expose the dashboard on a different path   |
| PermissionManagerResolver | AllowLocalAccessPermissionManager | Default permissions will only authorize api access for local requests |

## Authorization

By default, the api will authorize access only to local requests, and users will have full access permissions.

You can easily change authorization behaviour with the PermissionManageResolver option in the DashboardConfig. You will need to implement a new permision manager which implements the very simple IPermissionManager interface, and ensure it's registered it with the DI container.

### Sample Custom IPermissionManager

```csharp
public class CustomPermissionManager(IMyUserContext userContext) : IPermissionManager {

    public Task<Permission> CurrentPermission()
    {
        var currentUser = await userContext.GetCurrentUser();

        if (currentUser is null) return Permission.None;
        if (currentUser.IsAdministrator()) return Permission.All;

        return Permission.ViewDefinitions;
    }

}
```

```csharp
services.AddWorkflowCoreDashboard(new DashboardConfig()
{
	PermissionManagerResolver = new PermissionManagerResolver<CustomPermissionManager>()
};

services.AddTransient<CustomPermissionManager>();
```

## TODO

Workflow Core Dashboard is in early development. Still todo:

 - More workflow detail, filtering + search capabilities
 - List events, with filtering + search capabilities
 - List errors, with filtering + search capabilities
 - Overview of specific workflow instance. Show current status, step history, state etc
 - Suspend / resume / terminate workflows
 - UI improvements based on user permissions