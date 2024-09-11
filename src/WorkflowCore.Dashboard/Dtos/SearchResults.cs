namespace WorkflowCore.Dashboard.Dtos;

public record SearchResults<T>(int Page, IEnumerable<T> Items)
{
}
