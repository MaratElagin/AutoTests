using Xunit;
using Xunit.Extensions.Ordering;

namespace ZenkitAutoTests.Tests;

[Order(3)]
public class DeleteTaskTest : AuthBase
{
	[Fact]
	public void DeleteTask()
	{
		// Arrange
		App.NavigationHelper.GoToTasksPage();
		var task = new TaskData {Name = "Автотестирование №6"};
		// Act
		App.TaskHelper.DeleteTask(task);
		App.NavigationHelper.ReturnToTasksPage();
		var isTaskDeleted = App.TaskHelper.IsTaskDeleted(task);

		// Assert
		Assert.True(isTaskDeleted);
	}
}