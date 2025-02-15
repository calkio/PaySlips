using Moq;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Service;
using PaySlips.Core.Model.Parents;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Requests;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Responses;

namespace ServiceTeacher.Test
{
    public class ServiceCreateTeacherTest
    {
        [Fact]
        public async Task GetAllTeacherNoLesson_ValidRequest_ReturnsExpectedResponse()
        {
            // Arrange
            var request = new NoLessonTeacherRequest("dummy.xlsx");
            var expectedResponse = new NoLessonTeacherResponses(new List<Teacher>
            {
                new Teacher { Secondname = "Иванов", Firstname = "Иван", Surname = "Иванович" }
            });

            var mockServiceTeacher = new Mock<IServiceTeacher>();
            mockServiceTeacher
                .Setup(s => s.GetAllTeacherNoLesson(request))
                .ReturnsAsync(expectedResponse);

            var serviceTeacher = new ServiceTeacher(mockServiceTeacher.Object);

            // Act
            NoLessonTeacherResponses actualResponse = await serviceTeacher.GetAllTeacherNoLesson(request);

            // Assert
            Assert.Equal(expectedResponse, actualResponse);
            mockServiceTeacher.Verify(s => s.GetAllTeacherNoLesson(request), Times.Once);
        }

        [Fact]
        public async Task GetAllTeacherNoLesson_UnderlyingServiceThrows_ExceptionPropagates()
        {
            // Arrange
            var request = new NoLessonTeacherRequest("dummy.xlsx");
            var exception = new InvalidOperationException("Some error occurred");
            var mockServiceTeacher = new Mock<IServiceTeacher>();
            mockServiceTeacher
                .Setup(s => s.GetAllTeacherNoLesson(request))
                .ThrowsAsync(exception);

            var serviceTeacher = new ServiceTeacher(mockServiceTeacher.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await serviceTeacher.GetAllTeacherNoLesson(request);
            });
            Assert.Equal(exception.Message, ex.Message);
            mockServiceTeacher.Verify(s => s.GetAllTeacherNoLesson(request), Times.Once);
        }

        [Fact]
        public async Task GetAllTeacherNoLesson_NullRequest_DelegatesCall()
        {
            // Arrange
            NoLessonTeacherRequest request = null;
            // Симулируем поведение внутреннего сервиса при null-запросе (например, он выбрасывает ArgumentNullException)
            var mockServiceTeacher = new Mock<IServiceTeacher>();
            mockServiceTeacher
                .Setup(s => s.GetAllTeacherNoLesson(null))
                .ThrowsAsync(new ArgumentNullException(nameof(request)));

            var serviceTeacher = new ServiceTeacher(mockServiceTeacher.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await serviceTeacher.GetAllTeacherNoLesson(request);
            });
            mockServiceTeacher.Verify(s => s.GetAllTeacherNoLesson(null), Times.Once);
        }

        [Fact]
        public async Task GetAllTeacherNoLesson_CalledMultipleTimes_VerifyInvocationCount()
        {
            // Arrange
            var request = new NoLessonTeacherRequest("dummy.xlsx");
            var expectedResponse = new NoLessonTeacherResponses(new List<Teacher>
            {
                new Teacher { Secondname = "Иванов", Firstname = "Иван", Surname = "Иванович" }
            });

            var mockServiceTeacher = new Mock<IServiceTeacher>();
            mockServiceTeacher
                .Setup(s => s.GetAllTeacherNoLesson(request))
                .ReturnsAsync(expectedResponse);

            var serviceTeacher = new ServiceTeacher(mockServiceTeacher.Object);

            // Act
            var response1 = await serviceTeacher.GetAllTeacherNoLesson(request);
            var response2 = await serviceTeacher.GetAllTeacherNoLesson(request);

            // Assert
            Assert.Equal(expectedResponse, response1);
            Assert.Equal(expectedResponse, response2);
            mockServiceTeacher.Verify(s => s.GetAllTeacherNoLesson(request), Times.Exactly(2));
        }
    }
}
