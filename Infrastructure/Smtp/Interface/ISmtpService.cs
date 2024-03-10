namespace TaskManager.Infrastructure.Smtp;

public interface ISmtpService
{
    string SendEmail(string toAddress, string subject, string body);
}