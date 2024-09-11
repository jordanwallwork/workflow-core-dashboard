using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Models;

namespace WorkflowCore.Dashboard.Dtos;

public class WorkflowDto
{
    public string Id { get; }
    public DateTime Started { get; }
    public string Status { get; }
    public DateTime? Completed { get; }
    public int Version { get; }

    public WorkflowDto(WorkflowInstance workflow)
    {
        Id = workflow.Id;
        Started = workflow.CreateTime;
        Status = workflow.Status.ToString();
        Completed = workflow.CompleteTime;
        Version = workflow.Version;
    }
}
