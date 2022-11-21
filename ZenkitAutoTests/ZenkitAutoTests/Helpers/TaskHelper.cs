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
		Thread.Sleep(3000);
	}

	public void DeleteTask(TaskData task)
	{
		OpenTaskByName(task.Name);
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

	private void OpenLastCreatedTask()
	{
		var lastTask = GetAllTasks().Last();
		lastTask.FindElement(By.XPath(".//a")).Click();
	}

	private void OpenTaskByName(string name)
	{
		var allTasks = GetAllTasks();
		
		var taskName = allTasks.FirstOrDefault(el =>
			el.FindElement(By.XPath(".//span[@class='zenkit-badge-element-content']")).Text == name);
		taskName.FindElement(By.XPath("./parent::*/parent::*/parent::*/parent::*")).Click();
	}

	private ReadOnlyCollection<IWebElement> GetAllTasks()
	{
		Thread.Sleep(1000);
		return Driver.FindElements(By.XPath("//zenkit-kanban-view/div/div/div[1]/div/div[1]/div/div[1]/div[1]/div"));
	}
}