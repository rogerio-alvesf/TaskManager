using System.Net;
using System.Net.Mail;
using dotenv.net;

namespace TaskManager.Infrastructure.Smtp;

public class SmtpService : ISmtpService
{

    public string SendEmail(string toAddress, string subject, string body)
    {
        try
        {
            DotEnv.Load();

            string smtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER") ?? "";
            int smtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "0");
            string smtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME") ?? "";
            string smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? "";

            using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
            {

                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(smtpUsername);
                mailMessage.To.Add(toAddress);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                client.Send(mailMessage);
            }
            return $"Email sent successfully to {toAddress}";
        }
        catch (Exception ex)
        {
            throw new Exception($"Error when sending password reset email to ${ex.Message}");
        }
    }
}