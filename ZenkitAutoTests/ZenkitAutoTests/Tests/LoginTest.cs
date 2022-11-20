using Xunit;

namespace ZenkitAutoTests.Tests;

public class LoginTest : TestBase
{
	[Fact]
	public void Auth()
	{
		Authorization();
	}
}