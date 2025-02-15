using PaySlips.Core.Model.Parents;

namespace PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Responses
{
    public class NoLessonTeacherResponses
    {
        public IEnumerable<Teacher> Teachers { get; init; }

        public NoLessonTeacherResponses(IEnumerable<Teacher> teachers)
        {
            Teachers = teachers ?? throw new ArgumentNullException(nameof(teachers));
        }
    }
}
