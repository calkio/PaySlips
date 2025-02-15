namespace PaySlips.Core.Model.Teacher
{
    public class TimeSlot
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? EvenWeekSubject { get; set; }
        public string? OddWeekSubject { get; set; }
    }
}
