using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Requests;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Responses;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Service;
using PaySlips.Core.Model.Parents;
using ServiceTeacher.Parsers;

namespace ServiceTeacher.Implementation
{
    public class TeacherInFileNPOI : IServiceTeacher
    {
        public async Task<FilledTeacherResponses> GetAllFilledTeacher(FilledTeacherRequest request)
        {
            FilledLesson filledLesson = new FilledLesson(request);
            IEnumerable<Teacher> teachers = await filledLesson.FilledAllTeacher();
            var responses = new FilledTeacherResponses(teachers);
            return responses;
        }

        public async Task<NoLessonTeacherResponses> GetAllTeacherNoLesson(NoLessonTeacherRequest request)
        {
            CreateTeacher createTeacher = new CreateTeacher(request);
            IEnumerable<Teacher> teachers = await createTeacher.CreateAllTeacher();
            var responses = new NoLessonTeacherResponses(teachers);
            return responses;
        }
    }
}
