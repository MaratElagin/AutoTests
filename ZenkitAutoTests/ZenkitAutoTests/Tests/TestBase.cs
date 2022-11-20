using System;

namespace ZenkitAutoTests;

public class TestBase : IDisposable
{
	protected AppManager app;

	public TestBase()
	{
		app = new AppManager();
	}

	public void Dispose()
	{
		app.Stop();
	}

	protected void Authorization()
	{
		app.NavigationHelper.GoToLoginPage();

		var user = app.SecretsReader.GetAccountCredentialsFromSecretJson();
		app.LoginHelper.Login(user);
	}
}