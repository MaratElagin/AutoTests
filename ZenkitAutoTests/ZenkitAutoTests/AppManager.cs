using System;
using System.Threading;
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

	private AppManager()
	{
		_driver = new FirefoxDriver();
		_driver.Manage().Window.Maximize();
		_navigationHelper = new NavigationHelper(this);
		_loginHelper = new LoginHelper(this);
		_taskHelper = new TaskHelper(this);
		_baseUrl = "https://zenkit.com/en/todo/";
		_secretsReader = new SecretsReader();
	}

	public static AppManager GetInstance()
	{
		if (!app.IsValueCreated)
		{
			AppManager newInstance = new AppManager();
			app.Value = newInstance;
		}

		return app.Value;
	}
	
	private IWebDriver _driver { get; }
	private NavigationHelper _navigationHelper;
	private LoginHelper _loginHelper;
	private TaskHelper _taskHelper;
	private string _baseUrl;
	private SecretsReader _secretsReader { get; }
	private static ThreadLocal<AppManager> app = new();

	~AppManager()
	{
		try
		{
			Driver.Quit();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}
}