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
		var usernameField = FindElementWhenItsEnabled(By.Name("username"));
		usernameField.SendKeys(user.Username);

		var passwordField = FindElementWhenItsEnabled(By.Name("password"));
		passwordField.SendKeys(user.Password);

		FindElementWhenItsEnabled(By.CssSelector(".zenkit-login-button-row zenkit-ui-list-row-title")).Click();
		Thread.Sleep(3000);
	}
}