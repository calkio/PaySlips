using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Requests;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Responses;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Service;
using PaySlips.Core.Model.Lesson;
using ServiceTeacher.Implementation;

namespace ServiceTeacher.Test
{
    public class ServiceFilledTeacherTest_RealFile
    {
        private readonly string _filePath;

        public ServiceFilledTeacherTest_RealFile()
        {
            // Формируем путь к файлу, который должен лежать, например, в папке TestFiles
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFiles", "Scheduler.xls");
        }

        [Fact]
        public async Task GetAllFilledTeacher_RealFile_ReturnsExpectedTeachersWithLessonSchedules()
        {
            // Проверяем, что файл существует.
            Assert.True(File.Exists(_filePath), $"Файл не найден по пути: {_filePath}");

            // Arrange
            var request = new FilledTeacherRequest(_filePath, new List<Group>());

            // Используем реальную реализацию для чтения файла с заполненным расписанием
            IServiceTeacher realService = new TeacherInFileNPOI();
            var serviceTeacher = new ServiceTeacher(realService);

            // Act
            FilledTeacherResponses response = await serviceTeacher.GetAllFilledTeacher(request);
            var teachers = response.Teachers.ToList();

            // Assert
            // Если структура файла аналогична файлу без уроков, ожидаем, например, 22 преподавателя
            Assert.Equal(22, teachers.Count);

            // Проверяем данные первого преподавателя и наличие расписания
            var teacher1 = teachers[0];
            Assert.Equal("Уланов", teacher1.Secondname);
            Assert.Equal("Алексей", teacher1.Firstname);
            Assert.Equal("Александрович", teacher1.Surname);
            // Допустим, что в листе с первым преподавателем есть расписание (минимум 1 временной интервал)
            Assert.True(teacher1.TimeSlot.Count > 0, "У преподавателя не найдено ни одного временного интервала");

            // Если в одном из листов расписание задано (например, в листе с преподавателем "Петров Станислав Михайлович")
            // можно проверить конкретные значения:
            // Найдём преподавателя с фамилией "Петров"
            var petrovTeacher = teachers.FirstOrDefault(t => t.Secondname.Equals("Петров", StringComparison.OrdinalIgnoreCase));
            if (petrovTeacher != null && petrovTeacher.TimeSlot.Any())
            {
                var slot = petrovTeacher.TimeSlot[1];
                // Например, если ожидается, что день указан как "пятница" (DayOfWeek.Friday) и время "10:15"
                Assert.Equal(DayOfWeek.Friday, slot.DayOfWeek);
                Assert.Equal(TimeSpan.Parse("10:15"), slot.StartTime);
                // А также проверяем названия предметов
                Assert.Equal(null, slot.EvenWeekSubject);
                Assert.Equal(null, slot.OddWeekSubject);
            }
        }
    }
}
