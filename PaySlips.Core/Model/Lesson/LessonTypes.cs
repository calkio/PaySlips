namespace PaySlips.Core.Model.Lesson
{
    public class LessonTypes
    {
        public string Name { get; }

        private LessonTypes(string name)
        {
            Name = name;
        }

        public static readonly LessonTypes Normal = new LessonTypes("Normal");
        public static readonly LessonTypes DayOff = new LessonTypes("DayOff");
        public static readonly LessonTypes Credit = new LessonTypes("Credit");
        public static readonly LessonTypes Exam = new LessonTypes("Exam");

        public static readonly List<LessonTypes> AllTypes = new List<LessonTypes>
        {
            Normal, DayOff, Credit, Exam
        };
    }
}
