using Xunit;
using Xunit.Extensions.Ordering;

namespace ZenkitAutoTests.Tests;

[Order(2)]
public class AddTaskTest : TestBase
{
	[Fact]
	public void AddTask()
	{
		// Arrange
		app.NavigationHelper.GoToTasksPage();
		var task = new TaskData {Name = "Автотестирование №4"};

		// Act
		app.TaskHelper.AddTask(task);
		var createdTask = app.TaskHelper.GetCreatedTask();

		// Assert
		Assert.Equal(task, createdTask);
	}
}