using LessonModel = PaySlips.Core.Model.Lesson.Lesson;

namespace PaySlips.Core.Model.Parents
{
    public class Teacher : Human
    {
        public IEnumerable<LessonModel> Lessons { get; set; }
        public string AcademicDegree { get; set; }
        public string AcademicTitle { get; set; }
        public string JobTitle { get; set; }
    }
}
