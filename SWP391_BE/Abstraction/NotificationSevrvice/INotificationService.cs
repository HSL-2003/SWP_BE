namespace SWP391_BE.Abstraction.NotificationSevrvice
{
    public interface INotificationService
    {
        Task SendNotificationCreateBooking(string managerId, string message, CancellationToken cancellationToken);
        Task SendNotificationCancelBooking(string userId, string message, string bookingId, CancellationToken cancellationToken);
    }
}
