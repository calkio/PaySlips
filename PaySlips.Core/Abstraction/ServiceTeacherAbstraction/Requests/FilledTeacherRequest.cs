
namespace PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Requests
{
    public class FilledTeacherRequest
    {
        public string PathFileScheduler { get; init; }

        public FilledTeacherRequest(string pathFileScheduler)
        {
            PathFileScheduler = pathFileScheduler ?? throw new ArgumentNullException(nameof(pathFileScheduler));
        }
    }
}
