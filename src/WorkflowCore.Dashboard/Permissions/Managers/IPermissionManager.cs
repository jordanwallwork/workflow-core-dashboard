namespace WorkflowCore.Dashboard.Permissions.Managers;

public interface IPermissionManager
{
    Task<Permission> CurrentPermission();
}
