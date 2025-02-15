using Moq;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Service;
using PaySlips.Core.Model.Parents;
using Xunit;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ServiceTeacher.Implementation;

namespace ServiceTeacher.Test
{
    public class ServiceCreateTeacherTest
    {
        [Fact]
        public async Task GetAllTeacherNoLesson_CallsServiceTeacherMethod()
        {
            // Arrange
            var expectedTeachers = new List<Teacher>
            {
                new Teacher { Secondname = "Иванов", Firstname = "Иван", Surname = "Иванович" }
            };

            var mockServiceTeacher = new Mock<IServiceTeacher>();
            mockServiceTeacher
                .Setup(s => s.GetAllTeacherNoLesson(It.IsAny<string>()))
                .ReturnsAsync(expectedTeachers);

            var serviceTeacher = new ServiceTeacher(mockServiceTeacher.Object);

            // Act
            var result = await serviceTeacher.GetAllTeacherNoLesson("dummy.xlsx");

            // Assert
            Assert.Equal(expectedTeachers, result);
            mockServiceTeacher.Verify(s => s.GetAllTeacherNoLesson("dummy.xlsx"), Times.Once);
        }

        [Fact]
        public void Constructor_NullServiceTeacher_ThrowsArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new ServiceTeacher(null));
        }
    }

    // Тесты для реализации IServiceTeacher, которая читает данные из Excel-файла с помощью NPOI
    public class TeacherInFileNPOITests : IDisposable
    {
        private readonly string _tempFilePath;

        public TeacherInFileNPOITests()
        {
            // Создаем временный файл с расширением .xlsx
            _tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".xlsx");
            CreateTestExcelFile(_tempFilePath);
        }

        /// <summary>
        /// Создает тестовый Excel-файл с 3 листами:
        /// - Лист с индексом 0 не обрабатывается.
        /// - Листы с индексами 1 и 2 содержат данные преподавателей.
        /// В каждом листе на строке с индексом 4 (пятая строка) в ячейке с индексом 1 задается ФИО преподавателя.
        /// </summary>
        private void CreateTestExcelFile(string filePath)
        {
            // Создаем рабочую книгу (формат .xlsx)
            IWorkbook workbook = new XSSFWorkbook();

            // Лист 0: dummy (не обрабатывается)
            workbook.CreateSheet("DummySheet");

            // Лист 1: данные первого преподавателя
            var sheet1 = workbook.CreateSheet("TeacherSheet1");
            IRow row1 = sheet1.CreateRow(4);
            ICell cell1 = row1.CreateCell(1);
            cell1.SetCellValue("Иванов Иван Иванович");

            // Лист 2: данные второго преподавателя
            var sheet2 = workbook.CreateSheet("TeacherSheet2");
            IRow row2 = sheet2.CreateRow(4);
            ICell cell2 = row2.CreateCell(1);
            cell2.SetCellValue("Петров Петр Петрович");

            // Записываем рабочую книгу во временный файл
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);
            }
        }

        [Fact]
        public async Task GetAllTeacherNoLesson_ValidFile_ReturnsTeachers()
        {
            // Arrange
            IServiceTeacher teacherService = new TeacherInFileNPOI();

            // Act
            var teachers = await teacherService.GetAllTeacherNoLesson(_tempFilePath);
            var teacherList = teachers.ToList();

            // Assert
            // Ожидается, что будут обработаны листы с индексами 1 и 2 (2 преподавателя)
            Assert.Equal(2, teacherList.Count);

            // Проверяем данные первого преподавателя (с листа с индексом 1)
            Assert.Equal("Иванов", teacherList[0].Secondname);
            Assert.Equal("Иван", teacherList[0].Firstname);
            Assert.Equal("Иванович", teacherList[0].Surname);

            // Проверяем данные второго преподавателя (с листа с индексом 2)
            Assert.Equal("Петров", teacherList[1].Secondname);
            Assert.Equal("Петр", teacherList[1].Firstname);
            Assert.Equal("Петрович", teacherList[1].Surname);
        }

        [Fact]
        public async Task GetAllTeacherNoLesson_InvalidPath_ThrowsFileNotFoundException()
        {
            // Arrange
            IServiceTeacher teacherService = new TeacherInFileNPOI();
            string invalidPath = Path.Combine(Path.GetTempPath(), "nonexistentfile.xlsx");

            // Act & Assert
            await Assert.ThrowsAsync<FileNotFoundException>(async () =>
            {
                await teacherService.GetAllTeacherNoLesson(invalidPath);
            });
        }

        [Fact]
        public async Task GetAllTeacherNoLesson_NullPath_ThrowsArgumentNullException()
        {
            // Arrange
            IServiceTeacher teacherService = new TeacherInFileNPOI();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await teacherService.GetAllTeacherNoLesson(null);
            });
        }

        public void Dispose()
        {
            // Удаляем временный файл после завершения тестов
            if (File.Exists(_tempFilePath))
            {
                File.Delete(_tempFilePath);
            }
        }
    }
}
