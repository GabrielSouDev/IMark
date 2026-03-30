using IMark.Shared.Models.Enums;

namespace IMark.Shared.Models.DTO.TimeTrackings;

public class TimeEntryDTO
{
    public List<TimeCheckDTO> Checks { get; set; } = new List<TimeCheckDTO>();

    public TimeEntryStatus Status;
    public TimeSpan HoursWorked;
}
