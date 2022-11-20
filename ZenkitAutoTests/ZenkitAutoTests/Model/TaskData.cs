using System.ComponentModel.DataAnnotations;

namespace ZenkitAutoTests;

public class TaskData
{
    public string? Name { get; set; }
    
    [MaxLength(50)]
    public string? Description { get; set; }
}