using Xunit;

namespace ZenkitAutoTests.Tests;

public class AddTaskTest : TestBase
{
	[Fact]
	public void AddTask()
	{
		Authorization();

		var task = new TaskData {Name = "Автотестирование № 3"};
		app.TaskHelper.AddTask(task);
	}
}