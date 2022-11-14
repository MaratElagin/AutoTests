using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ZenkitAutoTests;

public class TestBase : IDisposable
{
    private IWebDriver _driver { get; }

    public TestBase()
    {
        _driver = new FirefoxDriver();
    }

    public void Dispose()
    {
        _driver.Quit();
    }

    protected void OpenHomePageAndMaximizeWindow()
    {
        _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl("https://zenkit.com/login");
    }

    protected void Login(AccountData user)
    {
        var usernameField = FindElementWhenItsEnabled(By.Name("username"));
        usernameField.SendKeys(user.Username);

        var passwordField = FindElementWhenItsEnabled(By.Name("password"));
        passwordField.SendKeys(user.Password);

        FindElementWhenItsEnabled(By.CssSelector(".zenkit-login-button-row zenkit-ui-list-row-title")).Click();
    }

    protected void AddEntity(TaskData task)
    {
        // Мои задачи
        var myTasks = FindElementWhenItsEnabled(By.CssSelector(".zenkit-resource-icon__inner--initials"));
        myTasks.Click();

        var card = FindElementWhenItsEnabled(
            By.CssSelector("div.zenkit-kanban-view__column:nth-child(1) > div:nth-child(1) > div:nth-child(2)"));
        Actions action = new Actions(_driver);
        
        // Hover на карточку, чтобы появилась textarea
        action.MoveToElement(card).Perform();
        
        var textArea =
            FindElementWhenItsEnabled(By.XPath("//zenkit-add-item-control/div/div[1]/div"));
        textArea.Click();
        textArea.SendKeys(task.Name);
        // Создать
        _driver.FindElement(By.CssSelector(".zenkit-add-item-control--active .zenkit-add-item-control__button"))
            .Click();
    }

    private IWebElement FindElementWhenItsEnabled(By locator)
    {
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        return wait.Until(ExpectedConditions.ElementExists(locator));
    }
}