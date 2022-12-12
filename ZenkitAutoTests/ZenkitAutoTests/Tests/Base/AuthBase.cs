namespace ZenkitAutoTests.Tests;

public class AuthBase : TestBase
{
	protected AuthBase()
	{
		var accountData = App.SecretsReader.GetAccountCredentialsFromSecretJson(true);
		App.LoginHelper.Login(accountData);
	}
}