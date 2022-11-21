﻿using Xunit;

namespace ZenkitAutoTests.Tests;

public class AddTaskTest : TestBase
{
	[Fact]
	public void AddTask()
	{
		app.NavigationHelper.GoToTasksPage();
		var task = new TaskData {Name = "Автотестирование №4"};
		app.TaskHelper.AddTask(task);

		var createdTask = app.TaskHelper.GetCreatedTask();

		Assert.Equal(task, createdTask);
	}
}