using Moq;
using ReservationService.Application.Events;
using ReservationService.Application.Features.Commands;
using ReservationService.Application.Features.Handlers;
using ReservationService.Application.Interfaces;
using ReservationService.Application.Interfaces.RabbitMQ;
using ReservationService.Domain.Entities;

namespace ReservationService.UnitTests;
public class CreateReservationCommandHandlerTests
{
    private readonly Mock<IReservationService> _reservationServiceMock;
    private readonly Mock<IMessageBus> _messageBusMock;
    private readonly CreateReservationCommandHandler _handler;

    public CreateReservationCommandHandlerTests()
    {
        _reservationServiceMock = new Mock<IReservationService>();
        _messageBusMock = new Mock<IMessageBus>();
        _handler = new CreateReservationCommandHandler(
            _reservationServiceMock.Object,
            _messageBusMock.Object);
    }

    [Fact]
    public async Task Handle_Should_CallReservationServiceAndPublishEvent_WhenCommandIsValid()
    {
        // ARRANGE (آماده‌سازی)

        // A valid command from the user.
        var command = new CreateReservationCommand("user-123", "event-456", 2);

        //  A reservation object that we expect the service to return.
        var reservationId = Guid.NewGuid();
        var fakeReservation = Reservation.Create(command.UserId, command.EventId, command.Quantity);

        // Use reflection to set the Id for our test scenario since the setter is private
        typeof(Reservation).GetProperty(nameof(Reservation.Id))
            .SetValue(fakeReservation, reservationId);

        // 3. Setup the mocks:
        // "Hey _reservationServiceMock, when you are called with ANY parameters,
        //  just return our fake reservation successfully."
        _reservationServiceMock
            .Setup(s => s.CreateReservationAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>()))
            .ReturnsAsync(fakeReservation);

        // "Hey _messageBusMock, we are just setting you up to expect a call.
        // The PublishAsync method returns a Task, so we return a completed task."
        _messageBusMock
            .Setup(b => b.PublishAsync(It.IsAny<ReservationRequestedEvent>()))
            .Returns(Task.CompletedTask);


        // ACT (اجرا)

        // Now, call the actual method we want to test.
        var result = await _handler.Handle(command, CancellationToken.None);


        // ASSERT (بررسی)

        //  Check if the returned Guid matches the one from our fake reservation.
        Assert.Equal(reservationId, result);

        // Verify that the CreateReservationAsync method on our service was called EXACTLY ONCE.
        _reservationServiceMock.Verify(s => s.CreateReservationAsync(
            command.UserId,
            command.EventId,
            command.Quantity),
            Times.Once);

        //  Verify that the PublishAsync method on our message bus was called EXACTLY ONCE.
        // We also check if the event published has the correct ReservationId.
        _messageBusMock.Verify(b => b.PublishAsync(
            It.Is<ReservationRequestedEvent>(e => e.ReservationId == reservationId)),
            Times.Once);
    }
}

