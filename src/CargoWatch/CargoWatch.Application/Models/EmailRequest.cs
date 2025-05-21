namespace CargoWatch.Application.Models;

public class EmailRequest
{
    public string? EmailFrom { get; set; }
    public string EmailTo { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
}
