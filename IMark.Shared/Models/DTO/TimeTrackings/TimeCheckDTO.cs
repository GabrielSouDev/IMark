using IMark.Shared.Models.Enums;
namespace IMark.Shared.Models.DTO.TimeTrackings;

public class TimeCheckDTO
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
}