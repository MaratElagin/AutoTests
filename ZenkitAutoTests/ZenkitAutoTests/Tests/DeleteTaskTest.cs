using Xunit;
using Xunit.Extensions.Ordering;

namespace ZenkitAutoTests.Tests;

[Order(3)]
public class DeleteTaskTest : TestBase
{
	[Fact]
	public void DeleteTask()
	{
		// Arrange
		app.NavigationHelper.ReturnToTasksPage();
		var task = new TaskData {Name = "Автотестирование №3"};
		// Act
		app.TaskHelper.DeleteTask(task);
		app.NavigationHelper.ReturnToTasksPage();
		var isTaskDeleted = app.TaskHelper.IsTaskDeleted(task);

		// Assert
		Assert.True(isTaskDeleted);
	}
}