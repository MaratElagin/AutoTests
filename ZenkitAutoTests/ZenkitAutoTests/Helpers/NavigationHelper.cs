using OpenQA.Selenium;

namespace ZenkitAutoTests;

public class NavigationHelper : HelperBase
{
	public NavigationHelper(AppManager manager) : base(manager)
	{
	}
	
	public void GoToLoginPage() => RedirectTo("https://zenkit.com/login");

	public void GoToTasksPage()
	{
		// Мои задачи
		var myTasks = FindElementWhenItsEnabled(By.CssSelector(".zenkit-list-badge_list-name"));
		myTasks.Click();
	}

	public void ReturnToTasksPage()
	{
		var myTasks = FindElementWhenItsEnabled(By.CssSelector(".zenkit-entry-details__navigation-link"));
		myTasks.Click();
	}

	private void RedirectTo(string url)
		=> Driver.Navigate().GoToUrl(url);
}