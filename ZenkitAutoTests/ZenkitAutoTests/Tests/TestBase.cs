using System;

namespace ZenkitAutoTests;

public class TestBase
{
	protected AppManager app;

	public TestBase()
	{
		app = AppManager.GetInstance();
	}

	protected void Authorization()
	{
		app.NavigationHelper.GoToLoginPage();

		var user = app.SecretsReader.GetAccountCredentialsFromSecretJson();
		app.LoginHelper.Login(user);
	}
}