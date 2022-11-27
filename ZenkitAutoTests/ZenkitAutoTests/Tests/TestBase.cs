using System;

namespace ZenkitAutoTests;

public class TestBase
{
	protected AppManager app;

	public TestBase()
	{
		app = AppManager.GetInstance();
	}
}