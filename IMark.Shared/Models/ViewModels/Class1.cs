using IMark.Shared.Models.Enums;

namespace IMark.Shared.Models.ViewModels;

public class TimeEntryGeneral
{
    public Guid Id { get; set; }
    public string? Date { get; set; }
    public List<string> Checks { get; set; } = new List<string>();
    public TimeEntryStatus Status;
    public TimeSpan HoursWorked;
}
