namespace ZenkitAutoTests;

public class NavigationHelper : HelperBase
{
	public NavigationHelper(AppManager manager) : base(manager)
	{
	}
	
	public void GoToLoginPage()
	{
		Driver.Navigate().GoToUrl("https://zenkit.com/login");
	}
}