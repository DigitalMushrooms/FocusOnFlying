using FocusOnFlying.Application.Common.Behaviours;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Drony.Commands.UtworzDrona;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FocusOnFlying.Application.UnitTests.Common.Behaviours
{
    public class LoggingBehaviourShould
    {
        private readonly Mock<ILogger<UtworzDronaCommand>> _logger;
        private readonly Mock<ICurrentUserService> _currentUserService;

        public LoggingBehaviourShould()
        {
            _logger = new Mock<ILogger<UtworzDronaCommand>>();
            _currentUserService = new Mock<ICurrentUserService>();
        }

        [Fact]
        public async Task RunLogInformationOnce()
        {
            var sut = new LoggingBehaviour<UtworzDronaCommand>(_logger.Object, _currentUserService.Object);
            _currentUserService.Setup(x => x.Login).Returns("a");

            await sut.Process(new UtworzDronaCommand(), new CancellationToken());

            _logger.Verify(x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }
    }
}
