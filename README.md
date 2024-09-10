# Workflow Core Dashboard

Simple dashboard to visualise [Workflow Core](https://github.com/danielgerlag/workflow-core) state

## Usage

Add the dashboard to your application by adding `UseWorkflowCoreDashboard()`:

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

By default you can access the dashboard at `/wfc-dashboard` (this path is configurable, see below)

## Configuration

You can pass a `DashboardConfig` into the `UseWorkflowCoreDashboard()`

| Property    | Default Value  | Notes                                                               |
|-------------|----------------|---------------------------------------------------------------------|
| RoutePrefix | /wfc-dashboard | Change this if you want to expose the dashboard on a different path |

## TODO

Workflow Core Dashboard is in early development. Still todo:

 - List workflows, with filtering + search capabilities
 - List events, with filtering + search capabilities
 - List errors, with filtering + search capabilities
 - Overview of specific workflow instance. Show current status, step history, state etc
 - Permissions / access controls
 - Suspend / resume / terminate workflows