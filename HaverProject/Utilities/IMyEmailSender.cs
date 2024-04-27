using HaverProject.ViewModel;

namespace HaverProject.Utilities
{
    public interface IMyEmailSender
    {
        Task SendOneAsync(string name, string email, string subject, string htmlMessage);
        Task SendToManyAsync(EmailMessage emailMessage);

    }
}
