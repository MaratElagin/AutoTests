using System;
using Microsoft.Extensions.Configuration;

namespace ZenkitAutoTests;

public class SecretsReader
{
    private IConfiguration _configuration { get; }
    
    public SecretsReader()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("secrets.json",false,true);
        _configuration = builder.Build();
    }

    public AccountData GetAccountCredentialsFromSecretJson(bool isValid)
    {
        var section = isValid ? "validAccountData" : "invalidAccountData";
        return _configuration.GetSection(section).Get<AccountData>()
               ?? throw new InvalidOperationException($"Can't get '{section}' from secrets.json");
    }
}