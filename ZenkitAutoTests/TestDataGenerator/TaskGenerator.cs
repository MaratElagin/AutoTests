using System.Text;
using System.Text.Json;
using ZenkitAutoTests;

namespace TestDataGenerator;

public class TaskGenerator
{
	private static string _text = "Мороз и солнце; день чудесный!" +
	                              "Еще ты дремлешь, друг прелестный —" +
	                              "Пора, красавица, проснись:" +
	                              "Открой сомкнуты негой взоры" +
	                              "Навстречу северной Авроры," +
	                              "Звездою севера явись!";

	public static void GenerateTaskByTaskName(string name)
	{
		char[] separators = {' ', '\n', ',', '.', '—', ':', ';', '?', '!', ')', '('};
		string[] words = _text
			.Split(separators,
				StringSplitOptions.RemoveEmptyEntries)
			.Distinct()
			.ToArray();

		var rand = new Random();
		var wordsAmount = rand.Next(0, 5);
		var builder = new StringBuilder();
		for (var i = 0; i < wordsAmount; i++)
		{
			var n = rand.Next(0, words.Length);
			builder.Append(words[n]);
		}

		var description = builder.ToString();
		var task = new TaskData(name, description);
		
		var json = JsonSerializer.Serialize(task);
		string jsonFilePath = "Tasks.json";
		File.WriteAllText(jsonFilePath, json);
	}
}