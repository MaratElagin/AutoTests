using System;
using System.Threading;
using OpenQA.Selenium;

namespace ZenkitAutoTests;

public class LoginHelper : HelperBase
{
	public LoginHelper(AppManager manager) : base(manager)
	{
	}

	public void Login(AccountData user)
	{
		if (IsLoggedIn())
		{
			if (IsLoggedIn(user.Username))
			{
				return;
			}
		}

		var usernameField = FindElementWhenItsEnabled(By.Name("username"));
		usernameField.SendKeys(user.Username);

		var passwordField = FindElementWhenItsEnabled(By.Name("password"));
		passwordField.SendKeys(user.Password);

		FindElementWhenItsEnabled(By.CssSelector(".zenkit-login-button-row zenkit-ui-list-row-title")).Click();
	}

	public bool IsLoginSuccess()
	{
		Thread.Sleep(2000);
		if (IsOnLoginPage())
			return false;
		var myWorkSpace = FindElementWhenItsEnabled(
			By.CssSelector(".zenkit-workspaces-workspace-container:nth-child(1) .zenkit-workspaces__workspace-name"));
		return myWorkSpace.Enabled;
	}

	public bool IsOnLoginPage() => Driver.Url == "https://base.zenkit.com/login";

	public void Logout()
	{
		FindElementWhenItsEnabled(By.CssSelector(".initials")).Click();
		FindElementWhenItsEnabled(By.CssSelector(".zenkit-ui-list__row--color-danger zenkit-ui-list-row-title"))
			.Click();
	}

	private bool IsLoggedIn()
	{
		try
		{
			Driver.FindElement(By.CssSelector(".initials"));
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			return false;
		}

		return true;
	}

	private bool IsLoggedIn(string username)
	{
		FindElementWhenItsEnabled(By.CssSelector(".initials")).Click();
		Thread.Sleep(2000);
		var usernameLabel =
			FindElementWhenItsEnabled(By.XPath(
					"/html/body/zenkit-ui-dialog/div/div/div/div/div/div/div[1]/div/div/div[1]/div/div/div/zenkit-ui-list-row-title/div/div[2]"))
				.Text;
		FindElementWhenItsEnabled(By.CssSelector(".zenkit-ui-dialog-window")).Click();
		return usernameLabel == username;
	}
}