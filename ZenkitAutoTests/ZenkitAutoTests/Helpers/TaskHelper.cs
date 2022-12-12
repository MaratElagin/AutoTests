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
		Thread.Sleep(1000);
		FindElementWhenItsEnabled(By.XPath(
				"/html/body/div[5]/div/div/div/div/div/div/div/zenkit-kanban-view/div/div/div[1]/div/div[1]/div/div[2]/zenkit-add-item-control/div/div[2]/div"))
			.Click();
	}

	public void AddTaskWithDescription(TaskData task)
	{
		AddTask(task);
		OpenLastCreatedTask();

		FindElementWhenItsEnabled(By.CssSelector("zenkit-ui-list-row-title > .zenkit-control-empty-message")).Click();
		FindElementWhenItsEnabled(By.CssSelector(".zenkit-control-textfield-input-multiline"))
			.SendKeys(task.Description);
		FindElementWhenItsEnabled(By.CssSelector(".zenkit-entry-detail__list")).Click();

		AppManager.NavigationHelper.ReturnToTasksPage();
	}

	public void DeleteTask(TaskData task)
	{
		if (!TryOpenTaskByName(task.Name))
			throw new InvalidOperationException($"There is no task with {task.Name}");
		FindElementWhenItsEnabled(By.CssSelector(".zi-options3")).Click();
		FindElementWhenItsEnabled(By.CssSelector(".zenkit-ui-list__row--color-danger zenkit-ui-list-row-title"))
			.Click();
	}

	public TaskData GetCreatedTask()
	{
		OpenLastCreatedTask();
		var name = FindElementWhenItsEnabled(By.CssSelector(".zenkit-details-view__display-string")).Text!;
		string? description;
		try
		{
			description = FindElementWhenItsEnabled(By.CssSelector(".zenkit-control-textfield-read-content")).Text;
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
		var taskName =
			allTasks.FirstOrDefault(el => el.FindElement(By.XPath(".//a/div[2]/div/span/span")).Text == name);

		if (taskName == null)
			return false;
		taskName.FindElement(By.XPath(".//ancestor::a")).Click();
		return true;
	}

	private ReadOnlyCollection<IWebElement> GetAllTasks()
	{
		Thread.Sleep(2000);
		return Driver.FindElements(By.XPath(
			"//div[contains(@class, 'virtual-scroller-canvas')]//div[contains(@class, 'virtual-scroller-canvas')]/div"));
	}
}