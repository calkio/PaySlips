namespace PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Requests
{
    public class NoLessonTeacherRequest
    {
        public string PathFileScheduler { get; init; }

        public NoLessonTeacherRequest(string pathFileScheduler)
        {
            PathFileScheduler = pathFileScheduler ?? throw new ArgumentNullException(nameof(pathFileScheduler));
        }
    }
}
