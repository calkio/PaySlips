using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Service;
using PaySlips.Core.Model.Parents;

namespace ServiceTeacher
{
    public class ServiceTeacher
    {
        private readonly IServiceTeacher _serviceTeacher;

        public ServiceTeacher(IServiceTeacher serviceTeacher)
        {
            _serviceTeacher = serviceTeacher ?? throw new ArgumentNullException(nameof(serviceTeacher));
        }

        public async Task<IEnumerable<Teacher>> GetAllTeacherNoLesson(string pathFile)
        {
            return await _serviceTeacher.GetAllTeacherNoLesson(pathFile);
        }
    }
}
