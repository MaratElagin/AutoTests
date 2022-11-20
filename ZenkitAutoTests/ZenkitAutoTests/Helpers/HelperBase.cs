using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ZenkitAutoTests;

public class HelperBase
{
	protected IWebDriver Driver;

	protected AppManager AppManager;

	protected IWebElement FindElementWhenItsEnabled(By locator)
	{
		var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
		return wait.Until(ExpectedConditions.ElementExists(locator));
	}

	public HelperBase(AppManager manager)
	{
		AppManager = manager;
		Driver = manager.Driver;
	}
}