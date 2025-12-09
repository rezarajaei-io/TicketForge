using AuthService.Application.Features.Auth.Commands;
using AuthService.Application.Features.Auth.Commands.RegisterUser;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace AuthService.UnitTests
{
    public class RegisterUserCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_CreateUser_When_DataIsValid()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var mockHasher = new Mock<IPasswordHasher>();

            // Mocks
            mockHasher.Setup(h => h.HashPassword(It.IsAny<string>())).Returns("hashed_secret");
            mockRepo.Setup(r => r.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((User?)null);

            var handler = new RegisterUserCommandHandler(mockRepo.Object, mockHasher.Object);
            var command = new RegisterUserCommand("reza", "reza@test.com", "123456");

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty(); // باید یک GUID برگرداند

            // بررسی اینکه آیا متد AddAsync فراخوانی شده است؟
            mockRepo.Verify(r => r.AddAsync(It.Is<User>(u => u.Username == "reza" && u.PasswordHash == "hashed_secret")), Times.Once);
        }
    }
}