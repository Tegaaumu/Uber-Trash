namespace Uber_Trash.SMTP
{
    public interface IEmailSender
    {
        void SendEmailAsync(string email, string subject, string message, string code);
    }
}
