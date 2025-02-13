namespace PaySlips.Core.Model.Lesson
{
    public class Lesson
    {
        public DateTime Date { get; set; }
        public Group Group { get; set; }
        public TimeSpan Duration { get; set; }
        public Discipline Discipline { get; set; }
        public string LessonTypes { get; set; }
    }
}
