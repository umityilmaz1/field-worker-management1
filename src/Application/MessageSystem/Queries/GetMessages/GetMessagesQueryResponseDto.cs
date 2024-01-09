namespace CleanArchitecture.Application.MessageSystem.Queries.GetMessages;
public class GetMessagesQueryResponseDto
{
    public Guid MessageId { get; set; }
    public string Content { get; set; }
    public DateTime SendDate { get; set; }
    public bool IsRead { get; set; }
}
