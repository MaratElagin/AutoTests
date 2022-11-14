using Xunit;
using ZenkitAutoTests;

public class Tests : TestBase
{
    private SecretsReader _secretsReader { get; }

    public Tests()
    {
        _secretsReader = new SecretsReader();
    }

    [Fact]
    public void Auth()
    {
        Authorization();
    }

    [Fact]
    public void AddTask()
    {
        Authorization();
    
        var task = new TaskData {Name = "Автотестирование № 2"};
        AddEntity(task);
    }

    private void Authorization()
    {
        OpenHomePageAndMaximizeWindow();

        var user = _secretsReader.GetAccountCredentialsFromSecretJson();
        Login(user);
    }
}