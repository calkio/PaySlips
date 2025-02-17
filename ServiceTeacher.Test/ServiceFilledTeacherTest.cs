using Moq;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Requests;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Responses;
using PaySlips.Core.Abstraction.ServiceTeacherAbstraction.Service;
using PaySlips.Core.Model.Lesson;
using PaySlips.Core.Model.Parents;

namespace ServiceTeacher.Test
{
    public class ServiceFilledTeacherTest
    {
        [Fact]
        public async Task GetAllFilledTeacher_ValidRequest_ReturnsExpectedResponse()
        {
            // Arrange
            var request = new FilledTeacherRequest("dummy.xlsx", new List<Group>());
            var expectedResponse = new FilledTeacherResponses(new List<Teacher>
            {
                new Teacher { Secondname = "Петров", Firstname = "Петр", Surname = "Петрович" }
            });

            var mockServiceTeacher = new Mock<IServiceTeacher>();
            mockServiceTeacher
                .Setup(s => s.GetAllFilledTeacher(request))
                .ReturnsAsync(expectedResponse);

            var serviceTeacher = new ServiceTeacher(mockServiceTeacher.Object);

            // Act
            FilledTeacherResponses actualResponse = await serviceTeacher.GetAllFilledTeacher(request);

            // Assert
            Assert.Equal(expectedResponse, actualResponse);
            mockServiceTeacher.Verify(s => s.GetAllFilledTeacher(request), Times.Once);
        }

        [Fact]
        public async Task GetAllFilledTeacher_UnderlyingServiceThrows_ExceptionPropagates()
        {
            // Arrange
            var request = new FilledTeacherRequest("dummy.xlsx", new List<Group>());
            var exception = new InvalidOperationException("Some error occurred in filled teacher");
            var mockServiceTeacher = new Mock<IServiceTeacher>();
            mockServiceTeacher
                .Setup(s => s.GetAllFilledTeacher(request))
                .ThrowsAsync(exception);

            var serviceTeacher = new ServiceTeacher(mockServiceTeacher.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await serviceTeacher.GetAllFilledTeacher(request);
            });
            Assert.Equal(exception.Message, ex.Message);
            mockServiceTeacher.Verify(s => s.GetAllFilledTeacher(request), Times.Once);
        }

        [Fact]
        public async Task GetAllFilledTeacher_NullRequest_DelegatesCall()
        {
            // Arrange
            FilledTeacherRequest request = null;
            // Симулируем поведение внутреннего сервиса при null-запросе (например, выбрасывается ArgumentNullException)
            var mockServiceTeacher = new Mock<IServiceTeacher>();
            mockServiceTeacher
                .Setup(s => s.GetAllFilledTeacher(null))
                .ThrowsAsync(new ArgumentNullException(nameof(request)));

            var serviceTeacher = new ServiceTeacher(mockServiceTeacher.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await serviceTeacher.GetAllFilledTeacher(request);
            });
            mockServiceTeacher.Verify(s => s.GetAllFilledTeacher(null), Times.Once);
        }

        [Fact]
        public async Task GetAllFilledTeacher_CalledMultipleTimes_VerifyInvocationCount()
        {
            // Arrange
            var request = new FilledTeacherRequest("dummy.xlsx", new List<Group>());
            var expectedResponse = new FilledTeacherResponses(new List<Teacher>
            {
                new Teacher { Secondname = "Петров", Firstname = "Петр", Surname = "Петрович" }
            });

            var mockServiceTeacher = new Mock<IServiceTeacher>();
            mockServiceTeacher
                .Setup(s => s.GetAllFilledTeacher(request))
                .ReturnsAsync(expectedResponse);

            var serviceTeacher = new ServiceTeacher(mockServiceTeacher.Object);

            // Act
            var response1 = await serviceTeacher.GetAllFilledTeacher(request);
            var response2 = await serviceTeacher.GetAllFilledTeacher(request);

            // Assert
            Assert.Equal(expectedResponse, response1);
            Assert.Equal(expectedResponse, response2);
            mockServiceTeacher.Verify(s => s.GetAllFilledTeacher(request), Times.Exactly(2));
        }
    }
}
