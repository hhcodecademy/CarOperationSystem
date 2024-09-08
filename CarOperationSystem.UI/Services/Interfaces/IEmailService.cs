namespace CarOperationSystem.UI.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendMail(string url, string toEmail);
    }
}
