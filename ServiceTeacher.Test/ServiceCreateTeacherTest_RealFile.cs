using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Requests;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Responses;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Service;
using ServiceTeacher.Implementation;

namespace ServiceTeacher.Test
{
    public class ServiceCreateTeacherTest_RealFile
    {
        private readonly string _filePath;

        public ServiceCreateTeacherTest_RealFile()
        {
            // Получаем путь к файлу из папки TestFiles, который должен быть скопирован в выходной каталог.
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFiles", "Scheduler.xls");
        }

        [Fact]
        public async Task GetAllTeacherNoLesson_RealFile_ReturnsExpectedTeacher()
        {
            // Проверяем, что файл существует.
            Assert.True(File.Exists(_filePath), $"Файл не найден по пути: {_filePath}");

            // Arrange
            var request = new NoLessonTeacherRequest(_filePath);

            // Используем реальную реализацию, которая парсит Excel-файл.
            IServiceTeacher realService = new TeacherInFileNPOI();
            var serviceTeacher = new ServiceTeacher(realService);

            // Act
            NoLessonTeacherResponses response = await serviceTeacher.GetAllTeacherNoLesson(request);
            var teachers = response.Teachers.ToList();

            // Assert
            // Ожидаем, что в файле 22 преподавателя (как в приведённом примере CSV)
            Assert.Equal(22, teachers.Count);

            // Проверяем данные нескольких преподавателей
            // Первый преподаватель: "Уланов Алексей Александрович"
            var teacher1 = teachers[0];
            Assert.Equal("Уланов", teacher1.Secondname);
            Assert.Equal("Алексей", teacher1.Firstname);
            Assert.Equal("Александрович", teacher1.Surname);

            // Второй преподаватель: "Смирнов Дмитрий Юрьевич"
            var teacher2 = teachers[1];
            Assert.Equal("Смирнов", teacher2.Secondname);
            Assert.Equal("Дмитрий", teacher2.Firstname);
            Assert.Equal("Юрьевич", teacher2.Surname);

            // Например, один из последних преподавателей: "Юданова Вера Валерьевна"
            var teacherLast = teachers.Last();
            Assert.Equal("Юданова", teacherLast.Secondname);
            Assert.Equal("Вера", teacherLast.Firstname);
            Assert.Equal("Валерьевна", teacherLast.Surname);
        }
    }
}
