using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace ZenkitAutoTests;

public class TaskHelper : HelperBase
{
	public TaskHelper(AppManager manager) : base(manager)
	{
	}

	public void AddTask(TaskData task)
	{
		var card = FindElementWhenItsEnabled(
			By.CssSelector("div.zenkit-kanban-view__column:nth-child(1) > div:nth-child(1) > div:nth-child(2)"));
		Actions action = new Actions(Driver);

		// Hover на карточку, чтобы появилась textarea
		action.MoveToElement(card).Perform();

		var textArea =
			FindElementWhenItsEnabled(By.XPath("//zenkit-add-item-control/div/div[1]/div"));
		textArea.Click();
		textArea.SendKeys(task.Name);
		// Создать
		Driver.FindElement(By.CssSelector(".zenkit-add-item-control--active .zenkit-add-item-control__button"))
			.Click();
	}

	public void DeleteTask(TaskData task)
	{
		if (!TryOpenTaskByName(task.Name))
			throw new InvalidOperationException($"There is no task with {task.Name}");
		Driver.FindElement(By.CssSelector(".zi-options3")).Click();
		Driver.FindElement(By.CssSelector(".zenkit-ui-list__row--color-danger zenkit-ui-list-row-title")).Click();
	}

	public TaskData GetCreatedTask()
	{
		OpenLastCreatedTask();
		var name = FindElementWhenItsEnabled(By.CssSelector(".zenkit-details-view__display-string")).Text!;
		string? description;
		try
		{
			description = FindElementWhenItsEnabled(By.TagName("//zenkit-ui-list-row-title/div/div")).Text;
		}
		catch (Exception)
		{
			description = null;
		}

		return new TaskData(name, description);
	}

	public bool IsTaskDeleted(TaskData task)
	{
		return !TryOpenTaskByName(task.Name);
	}

	private void OpenLastCreatedTask()
	{
		var lastTask = GetAllTasks().Last();
		lastTask.FindElement(By.XPath(".//a")).Click();
	}

	private bool TryOpenTaskByName(string name)
	{
		var allTasks = GetAllTasks();
		foreach (var task in allTasks)
		{
			var c = task.FindElement(By.XPath(".//a/div[2]/div/span/span"));
			var find = c.Text == name;
		}

		var taskName =
			allTasks.First(el => el.FindElement(By.XPath(".//a/div[2]/div/span/span")).Text == name);
		var t = allTasks.First();
		var k = t.FindElement(By.XPath(".//a/div[2]/div/span/span"));
		// var taskName = allTasks.FirstOrDefault(el =>
		// 	el.FindElement(By.XPath(".//span[contains(@class, 'zenkit-badge-element-content')]")).Text == name);
		if (t == null)
			return false;
		t.FindElement(By.XPath("./parent::*/parent::*/parent::*/parent::*")).Click();
		return true;
	}

	private ReadOnlyCollection<IWebElement> GetAllTasks()
	{
		Thread.Sleep(2000);
		return Driver.FindElements(By.XPath(
			"//div[contains(@class, 'virtual-scroller-canvas')]//div[contains(@class, 'virtual-scroller-canvas')]/div"));
	}
}