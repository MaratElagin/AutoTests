using Xunit;
using Xunit.Extensions.Ordering;

namespace ZenkitAutoTests.Tests;

[Order(1)]
public class LoginTests : TestBase
{
	[Fact]
	public void LoginWithInvalidData()
	{
		// Arrange
		if (!App.LoginHelper.IsOnLoginPage())
			App.NavigationHelper.GoToLoginPage();
		var invalidAccountData = App.SecretsReader.GetAccountCredentialsFromSecretJson(false);

		// Act
		App.LoginHelper.Login(invalidAccountData);
		var actual = App.LoginHelper.IsLoginSuccess();

		// Assert
		Assert.False(actual);
	}
	
	[Fact]
	public void LoginWithValidData()
	{
		// Arrange
		if (!App.LoginHelper.IsOnLoginPage())
			App.NavigationHelper.GoToLoginPage();
		var validAccountData = App.SecretsReader.GetAccountCredentialsFromSecretJson(true);
		
		// Act
		App.LoginHelper.Login(validAccountData);
		var actual = App.LoginHelper.IsLoginSuccess();

		// Assert
		Assert.True(actual);
	}
}