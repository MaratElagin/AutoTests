namespace ZenkitAutoTests;

public class TestBase
{
	protected readonly AppManager App;

	protected TestBase()
	{
		App = AppManager.GetInstance();
		App.NavigationHelper.GoToLoginPage();
	}
}