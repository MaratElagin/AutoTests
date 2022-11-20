using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace ZenkitAutoTests;

public class AppManager
{
	public IWebDriver Driver => _driver;
	public NavigationHelper NavigationHelper => _navigationHelper;
	public LoginHelper LoginHelper => _loginHelper;
	public TaskHelper TaskHelper => _taskHelper;
	public string BaseUrl => _baseUrl;
	public SecretsReader SecretsReader => _secretsReader;

	public AppManager()
	{
		_driver = new FirefoxDriver();
		_driver.Manage().Window.Maximize();
		_navigationHelper = new NavigationHelper(this);
		_loginHelper = new LoginHelper(this);
		_taskHelper = new TaskHelper(this);
		_baseUrl = "https://zenkit.com/en/todo/";
		_secretsReader = new SecretsReader();
	}

	public void Stop()
	{
		Driver.Quit();
	}

	private IWebDriver _driver { get; }
	private NavigationHelper _navigationHelper;
	private LoginHelper _loginHelper;
	private TaskHelper _taskHelper;
	private string _baseUrl;
	private SecretsReader _secretsReader { get; }
}