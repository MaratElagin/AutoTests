using System.IO;
using System.Text.Json;

namespace ZenkitAutoTests;

public class JsonReader
{
	private readonly string _jsonFilePath =
		@"D:\Marat\Study\Repository\AutoTests\ZenkitAutoTests\TestDataGenerator\bin\Debug\net6.0\Tasks.json";

	public TaskData GetTaskDataFromJsonFile()
	{
		var jsonFile = File.ReadAllText(_jsonFilePath);

		var taskData = JsonSerializer.Deserialize<TaskData>(jsonFile);
		return taskData;
	}
}