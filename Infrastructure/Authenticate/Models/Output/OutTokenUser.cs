namespace TaskManager.Infrastructure.Token;

public class OutTokenUser
{
    public string Token { get; set; }
    public OutTokenUser(string token) => Token = token;
}