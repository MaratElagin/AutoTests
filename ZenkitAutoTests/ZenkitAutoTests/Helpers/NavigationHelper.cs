using OpenQA.Selenium;

namespace ZenkitAutoTests;

public class NavigationHelper : HelperBase
{
	public NavigationHelper(AppManager manager) : base(manager)
	{
	}

	public void OpenHomePage() => RedirectTo("https://zenkit.com/en/todo/");

	public void GoToLoginPage() => RedirectTo("https://zenkit.com/login");

	public void GoToTasksPage()
	{
		// Мои задачи
		var myTasks = FindElementWhenItsEnabled(By.CssSelector(".zenkit-list-badge_list-name"));
		myTasks.Click();
	}

	private void RedirectTo(string url)
		=> Driver.Navigate().GoToUrl(url);
}