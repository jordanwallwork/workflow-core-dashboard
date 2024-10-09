namespace WorkflowCore.Dashboard.Permissions;

[Flags]
public enum Permission
{
    None = 1 << 0,
    ViewDefinitions = 1 << 1,
    ViewWorkflowInstances = 1 << 2,

    ViewAll = ViewDefinitions | ViewWorkflowInstances,

    All = ViewAll
}
