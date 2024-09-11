using WorkflowCore.Models;

namespace WorkflowCore.Dashboard.Filters;
public class WorkflowFilters
{
    public int Page { get; set; } = 1;

    public WorkflowStatus? Status { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }
}
