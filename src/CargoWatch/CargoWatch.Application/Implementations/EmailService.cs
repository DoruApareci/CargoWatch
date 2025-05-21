using CargoWatch.Application.Interfaces;
using CargoWatch.Application.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CargoWatch.Application.Implementations;

public class EmailService : IEmailService
{
    private readonly SendGridSettings _config;

    private readonly SendGridClient _client;

    public EmailService(IOptions<SendGridSettings> options)
    {
        _config = options.Value;
        _client = new SendGridClient(_config.ApiKey);
    }

    public EmailRequest FormEmailRequest(string? emailFrom, string emailTo, string subject, string body)
    {
        return new EmailRequest
        {
            EmailFrom = emailFrom ?? _config.DefaultEmail,
            EmailTo = emailTo,
            Subject = subject,
            Body = body
        };
    }

    public async Task SendEmail(EmailRequest emailRequest)
    {
        var emailFromAddress = new EmailAddress(emailRequest.EmailFrom ?? _config.DefaultEmail, _config.FromName);
        var emailToAddress = new EmailAddress(emailRequest.EmailTo);
        var message = MailHelper.CreateSingleEmail(emailFromAddress, emailToAddress, emailRequest.Subject, "", emailRequest.Body);
        var response = await _client.SendEmailAsync(message);
    }
}
