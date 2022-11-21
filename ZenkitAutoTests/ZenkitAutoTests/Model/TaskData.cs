using System.ComponentModel.DataAnnotations;

namespace ZenkitAutoTests;

public record TaskData
{
	public string Name { get; set; }

	[MaxLength(50)] public string? Description { get; }

	public TaskData()
	{
	}

	public TaskData(string name, string? description)
	{
		Name = name;
		Description = description;
	}
}