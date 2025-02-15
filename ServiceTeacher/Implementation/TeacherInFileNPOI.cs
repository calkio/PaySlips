using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Service;
using PaySlips.Core.Model.Parents;
using ServiceTeacher.Parsers;

namespace ServiceTeacher.Implementation
{
    public class TeacherInFileNPOI : IServiceTeacher
    {
        public async Task<IEnumerable<Teacher>> GetAllTeacherNoLesson(string pathFile)
        {
            CreateTeacher createTeacher = new CreateTeacher(pathFile);
            return await createTeacher.CreateAllTeacher();
        }
    }
}
