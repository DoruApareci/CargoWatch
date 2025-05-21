using CargoWatch.Application.Models;

namespace CargoWatch.Application.Interfaces;

//define email service interface
public interface IEmailService
{
    public EmailRequest FormEmailRequest(string? emailFrom, string emailTo, string subject, string body);
    public Task SendEmail(EmailRequest emailRequest);
}
