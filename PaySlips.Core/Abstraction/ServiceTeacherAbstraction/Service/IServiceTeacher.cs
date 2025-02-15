using PaySlips.Core.Model.Parents;

namespace PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Service
{
    public interface IServiceTeacher
    {
        Task<IEnumerable<Teacher>> GetAllTeacherNoLesson(string pathFile);
    }
}
