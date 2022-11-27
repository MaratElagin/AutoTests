using Xunit;
using Xunit.Extensions.Ordering;

namespace ZenkitAutoTests.Tests;

[Order(1)]
public class LoginTest : TestBase
{
	[Fact]
	public void Auth()
	{
		// Arrange
		app.NavigationHelper.GoToLoginPage();
		var user = app.SecretsReader.GetAccountCredentialsFromSecretJson();

		// Act
		app.LoginHelper.Login(user);
		var actual = app.LoginHelper.IsLoginSuccess();

		// Assert
		Assert.True(actual);
	}
}