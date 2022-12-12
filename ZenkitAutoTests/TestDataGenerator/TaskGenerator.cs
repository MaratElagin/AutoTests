using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
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

	public static void GenerateTask()
	{
		char[] separators = {' ', '\n', ',', '.', '—', ':', ';', '?', '!', ')', '('};
		string[] words = _text
			.Split(separators,
				StringSplitOptions.RemoveEmptyEntries)
			.Distinct()
			.Select(word => word.ToLower())
			.ToArray();
		var rand = new Random();

		var wordsAmount = rand.Next(0, 5);
		var builder = new StringBuilder();
		for (var i = 0; i < wordsAmount; i++)
		{
			var n = rand.Next(0, words.Length);
			if (i != wordsAmount - 1)
				builder.Append($"{words[n]} ");
			else builder.Append(words[n]);
		}

		var name = words[rand.Next(0, words.Length)];
		var description = builder.ToString();
		var task = new TaskData(name, description);


		JsonSerializerOptions options = new JsonSerializerOptions()
		{
			Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
			WriteIndented = true
		};

		var json = JsonSerializer.Serialize(task, options);
		string jsonFilePath = "Tasks.json";
		File.WriteAllText(jsonFilePath, json);
	}
}