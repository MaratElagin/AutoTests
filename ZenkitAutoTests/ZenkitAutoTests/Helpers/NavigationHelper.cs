using System.Threading;
using OpenQA.Selenium;

namespace ZenkitAutoTests;

public class NavigationHelper : HelperBase
{
	public NavigationHelper(AppManager manager) : base(manager)
	{
	}

	public void GoToLoginPage() => RedirectTo("https://base.zenkit.com/login");

	public void GoToTasksPage()
	{
		Thread.Sleep(2000);
		// Мои задачи
		var myTasks = FindElementWhenItsEnabled(By.XPath("//a[contains(@class, 'zenkit-undraggable-link')]"));
		myTasks.Click();
	}

	public void ReturnToTasksPage()
	{
		Driver.Navigate().Back();
	}

	private void RedirectTo(string url)
		=> Driver.Navigate().GoToUrl(url);
}