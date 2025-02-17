using LessonModel = PaySlips.Core.Model.Lesson.Lesson;

namespace PaySlips.Core.Model.Teacher
{
    public class TimeSlot
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public LessonModel? EvenWeekSubject { get; set; }
        public LessonModel? OddWeekSubject { get; set; }
    }
}
