namespace HitecService.Models.Configuration;

public class ApplicationConfiguration
{
    public ConnectionStrings? ConnectionStrings { get; set; }
    public string? ClientUrl { get; set; }

    public EmailCredentials? EmailCredentials { get; set; }
}

public class ConnectionStrings
{
    public string? SqlServerConnection { get; set; }
}

public class EmailCredentials
{
    public string? From { get; set; }
    public string? Password { get; set; }
    public string? Smtp { get; set; }
    public int Port { get; set; }
}