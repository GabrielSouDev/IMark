using IMark.Shared.Models.Enums;

namespace IMark.Shared.Models.DTO.TimeTrackings;

public class TimeEntryDTO
{
    public Guid Id { get; set; }
    public List<TimeCheckDTO> Checks { get; set; } = new List<TimeCheckDTO>();
    public TimeEntryStatus Status { get; set; }
    public TimeSpan HoursWorked { get; set; }
}
