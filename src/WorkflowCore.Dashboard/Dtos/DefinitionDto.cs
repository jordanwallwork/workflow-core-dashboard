using WorkflowCore.Models;

namespace WorkflowCore.Dashboard.Dtos;

public class DefinitionDto
{
    public string Id { get; }
    public int Version { get; }
    public string Description { get; }
    public IEnumerable<StepDto> Steps { get; }

    public DefinitionDto(WorkflowDefinition def)
    {
        Id = def.Id;
        Version = def.Version;
        Description = def.Description;
        Steps = def.Steps.Select(x => new StepDto(x));
    }
}
