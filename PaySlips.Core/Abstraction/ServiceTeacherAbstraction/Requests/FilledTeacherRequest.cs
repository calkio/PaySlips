
using PaySlips.Core.Model.Lesson;

namespace PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Requests
{
    public class FilledTeacherRequest
    {
        public string PathFileScheduler { get; init; }
        public IEnumerable<Group> Groups { get; init; }

        public FilledTeacherRequest(string pathFileScheduler, IEnumerable<Group> groups)
        {
            PathFileScheduler = pathFileScheduler ?? throw new ArgumentNullException(nameof(pathFileScheduler));
            Groups = groups ?? throw new ArgumentNullException("groups null");
        }
    }
}
