using Xunit;

namespace ZenkitAutoTests.Tests;

public class DeleteTaskTest : TestBase
{
	[Fact]
	public void DeleteTask()
	{
		app.NavigationHelper.GoToTasksPage();
		var task = new TaskData {Name = "Автотестирование №3"};
		app.TaskHelper.DeleteTask(task);

		var createdTask = app.TaskHelper.GetCreatedTask();

		Assert.Equal(task, createdTask);
	}
}