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
		// Мои задачи
		var myTasks = FindElementWhenItsEnabled(By.CssSelector(".zenkit-resource-icon__inner--initials"));
		myTasks.Click();

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
}