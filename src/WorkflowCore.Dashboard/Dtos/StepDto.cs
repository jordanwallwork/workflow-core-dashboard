using WorkflowCore.Models;

namespace WorkflowCore.Dashboard.Dtos;

public class StepDto
{
    public int Id { get; }
    public string? BodyType { get; }
    public string Name { get; }
    public string ExternalId { get; }
    public IEnumerable<int> Children { get; }

    // dev purposes only probably, delete these
    public IEnumerable<string?> Outcomes { get; }
    public int NumInputs { get; }
    public int NumOutputs { get; }

    public StepDto(WorkflowStep step)
    {
        Id = step.Id;
        BodyType = step.BodyType?.Name;
        Name = step.Name;
        ExternalId = step.ExternalId;
        Children = step.Children;

        // dev purposes only probably, delete these
        Outcomes = step.Outcomes.Select(x => x.Label);
        NumInputs = step.Inputs.Count;
        NumOutputs = step.Outputs.Count;
    }
}
