namespace PaySlips.Core.Model.Lesson
{
    public class Group
    {
        public Direction Direction { get; set; }
        public int Code { get; set; }
        public string Name { get => $"{Direction?.AbbreviatedName}-{(Code == 0 ? string.Empty : Code)}" ?? string.Empty; }
        public int CountStudents { get; set; }
        public string LearningType { get; set; }
    }
}
