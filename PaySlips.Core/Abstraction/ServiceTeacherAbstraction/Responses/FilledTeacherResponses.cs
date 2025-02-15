using PaySlips.Core.Model.Parents;

namespace PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Responses
{
    public class FilledTeacherResponses
    {
        public IEnumerable<Teacher> Teachers { get; init; }

        public FilledTeacherResponses(IEnumerable<Teacher> teachers)
        {
            Teachers = teachers ?? throw new ArgumentNullException(nameof(teachers));
        }
    }
}
