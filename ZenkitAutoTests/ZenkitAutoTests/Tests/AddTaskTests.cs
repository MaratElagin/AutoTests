using System.IO;
using System.Text.Json;
using System.Threading;
using Xunit;
using Xunit.Extensions.Ordering;

namespace ZenkitAutoTests.Tests;

[Order(2)]
public class AddTaskTests : AuthBase
{
	private readonly string _jsonFilePath =
		@"D:\Marat\Study\Repository\AutoTests\ZenkitAutoTests\TestDataGenerator\bin\Debug\net6.0\Tasks.json";

	private readonly string _taskDataJsonFile;

	public AddTaskTests()
	{
		_taskDataJsonFile = File.ReadAllText(_jsonFilePath);
	}

	[Fact]
	public void AddTask()
	{
		// Arrange
		Thread.Sleep(1000);
		if (App.Driver.Url == "https://base.zenkit.com/")
			App.NavigationHelper.GoToTasksPage();
		else App.NavigationHelper.ReturnToTasksPage();
		var task = new TaskData {Name = "Автотестирование №6"};

		// Act
		App.TaskHelper.AddTask(task);
		var createdTask = App.TaskHelper.GetCreatedTask();

		// Assert
		Assert.Equal(task, createdTask);
	}

	[Fact]
	public void CreateTaskFromJsonFile()
	{
		// Arrange
		Thread.Sleep(1000);
		if (App.Driver.Url == "https://base.zenkit.com/")
			App.NavigationHelper.GoToTasksPage();
		else App.NavigationHelper.ReturnToTasksPage();
		var task = GetTaskDataFromJson(_taskDataJsonFile);

		// Act
		App.TaskHelper.AddTaskWithDescription(task);
		var createdTask = App.TaskHelper.GetCreatedTask();

		// Assert
		Assert.Equal(task, createdTask);
	}

	private TaskData GetTaskDataFromJson(string jsonFile)
	{
		var taskData = JsonSerializer.Deserialize<TaskData>(jsonFile);
		return taskData;
	}
}