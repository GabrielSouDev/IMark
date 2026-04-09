using IMark.Shared.Models.Enums;
namespace IMark.Shared.Models.DTO.TimeTrackings;

public class TimeCheckDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Timestamp { get; set; }
    public DateTime TimestampLocal
    {
        get => DateTime.SpecifyKind(Timestamp, DateTimeKind.Utc).ToLocalTime();
        set => Timestamp = DateTime.SpecifyKind(value, DateTimeKind.Local).ToUniversalTime();
    }
}