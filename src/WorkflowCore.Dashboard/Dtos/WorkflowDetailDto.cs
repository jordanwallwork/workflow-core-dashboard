using WorkflowCore.Models;

namespace WorkflowCore.Dashboard.Dtos;

public class WorkflowDetailDto : WorkflowDto
{
    public object Data { get; }

    public WorkflowDetailDto(WorkflowInstance workflow) : base(workflow)
    {
        Data = workflow.Data;
    }
}