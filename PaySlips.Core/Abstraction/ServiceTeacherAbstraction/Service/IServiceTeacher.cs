using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Requests;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Responses;

namespace PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Service
{
    public interface IServiceTeacher
    {
        Task<NoLessonTeacherResponses> GetAllTeacherNoLesson(NoLessonTeacherRequest request);
        Task<FilledTeacherResponses> GetAllFilledTeacher(FilledTeacherRequest request);
    }
}
