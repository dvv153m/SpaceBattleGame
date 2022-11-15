using Moq;
using SpaceBattleGame.Contracts.Commands;
using SpaceBattleGame.Server.Commands.Fuel;
using SpaceBattleGame.Server.Exceptions;

namespace SpaceBattleGame.Server.Tests.Commands
{
    public class BurnFuelCommandTest
    {
        [Fact]
        public void BurnFuelCommand_FuelSpent_FuelDecreased()
        {
            Mock<IFuelabel> mockFuelable = new Mock<IFuelabel>();
            mockFuelable.SetupGet<int>(m => m.FuelVolume).Returns(10);
            mockFuelable.SetupGet<int>(m => m.FuelRate).Returns(1);

            new BurnFuelCommand(mockFuelable.Object).Execute();

            mockFuelable.VerifySet(m => m.FuelVolume = 9);
        }

        [Fact]
        public void BurnFuelCommand_FuelSpentIsZero_ShouldThrowCommandException()
        {
            Mock<IFuelabel> mockFuelable = new Mock<IFuelabel>();
            mockFuelable.SetupGet<int>(m => m.FuelVolume).Returns(10);
            mockFuelable.SetupGet<int>(m => m.FuelRate).Returns(0);

            //Act            
            var exception = Assert.Throws<CommandException>(
                () =>
                {
                    new BurnFuelCommand(mockFuelable.Object).Execute();
                });

            //Assert
            Assert.NotNull(exception);            
        }
    }
}
