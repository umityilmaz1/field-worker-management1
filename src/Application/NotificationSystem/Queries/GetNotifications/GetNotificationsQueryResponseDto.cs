namespace CleanArchitecture.Application.NotificationSystem.Queries.GetNotifications;
public class GetNotificationsQueryResponseDto
{
    public Guid NotificationId { get; set; }
    public string Content { get; set; }
    public DateTime SendDate { get; set; }
    public bool IsRead { get; set; }
    public bool IsEmergency { get; set; }
}
