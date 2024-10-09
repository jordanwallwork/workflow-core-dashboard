# Workflow Core Dashboard

Simple dashboard to visualise [Workflow Core](https://github.com/danielgerlag/workflow-core) state

**Note:** This is in early development, see the [roadmap](#todo) below 

## Installation

Install the NuGet package "[WorkflowCore.Dashboard](https://www.nuget.org/packages/WorkflowCore.Dashboard)" 

Using NuGet

	PM> Install-Package WorkflowCore.Dashboard
	
Using .net cli

	dotnet add package WorkflowCore.Dashboard

![NuGet Version](https://img.shields.io/nuget/v/WorkflowCore.Dashboard) ![NuGet Downloads](https://img.shields.io/nuget/dt/WorkflowCore.Dashboard)

## Usage

Add the dashboard to your application by adding `AddWorkflowCoreDashboard()` and `UseWorkflowCoreDashboard()`:

```csharp

public void ConfigureServices(IServiceCollection services)
{
	...
    
    // Register with DI container
    services.AddWorkflowCoreDashboard(/*optionally pass in DashboardConfig here*/);
    
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

| Property                  | Type                                     | Default Value                     | Notes                                                                 |
|---------------------------|------------------------------------------|-----------------------------------|-----------------------------------------------------------------------|
| RoutePrefix               | string                                   | /wfc-dashboard                    | Change this if you want to expose the dashboard on a different path   |
| PermissionManager         | Type (must implement IPermissionManager) | AllowLocalAccessPermissionManager | Default permissions will only authorize api access for local requests |
| PermissionManagerLifetime | ServiceLifetime                          |                                   | If provided, will register the permission manager with the DI container. Otherwise, will assume permission manager will be registered elsewhere |

## Authorization

By default, the api will authorize access only to local requests, and users will have full access permissions.

You can easily change authorization behaviour by implementing a new `IPermissionManager`. If you specify a lifetime then it will be registered with the DI container, otherwise it is assumed that it will be registered elsewhere.

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
	PermissionManager = typeof(CustomPermissionManager),
	PermissionManagerLifetime = ServiceLifetime.Scoped // not required if the IPermissionManager is registered with the DI container elsewhere
};
```

## TODO

Workflow Core Dashboard is in early development. Still todo:

 - More workflow detail, filtering + search capabilities
 - List events, with filtering + search capabilities
 - List errors, with filtering + search capabilities
 - Overview of specific workflow instance. Show current status, step history, state etc
 - Suspend / resume / terminate workflows
 - UI improvements based on user permissions