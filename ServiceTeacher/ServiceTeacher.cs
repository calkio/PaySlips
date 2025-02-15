using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Requests;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Responses;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Service;

namespace ServiceTeacher
{
    public class ServiceTeacher
    {
        private readonly IServiceTeacher _serviceTeacher;

        public ServiceTeacher(IServiceTeacher serviceTeacher)
        {
            _serviceTeacher = serviceTeacher ?? throw new ArgumentNullException(nameof(serviceTeacher));
        }

        public async Task<NoLessonTeacherResponses> GetAllTeacherNoLesson(NoLessonTeacherRequest request)
        {
            return await _serviceTeacher.GetAllTeacherNoLesson(request);
        }
        public async Task<FilledTeacherResponses> GetAllFilledTeacher(FilledTeacherRequest request)
        {
            return await _serviceTeacher.GetAllFilledTeacher(request);
        }
    }
}
