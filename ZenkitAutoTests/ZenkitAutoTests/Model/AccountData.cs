namespace ZenkitAutoTests;

public class AccountData
{
    public string Username { get; }
    public string Password { get; }

    public AccountData(string username, string password)
    {
        Username = username;
        Password = password;
    }
}