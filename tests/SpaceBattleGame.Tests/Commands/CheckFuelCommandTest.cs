using Moq;
using SpaceBattleGame.Contracts.Commands;
using SpaceBattleGame.Server.Commands.Fuel;
using SpaceBattleGame.Server.Exceptions;

namespace SpaceBattleGame.Server.Tests.Commands
{
    public class CheckFuelCommandTest
    {
        [Fact]
        public void CheckFuelCommand_FuelIsEmpty_ThrowNewCommaandException()
        {
            Mock<IFuelabel> mockFuelable = new Mock<IFuelabel>();
            mockFuelable.SetupGet<int>(m => m.FuelVolume).Returns(1);
            mockFuelable.SetupGet<int>(m => m.FuelRate).Returns(2);

            var exception = Assert.Throws<CommandException>(
                        () =>
                        {
                            new CheckFuelCommand(mockFuelable.Object).Execute();        
                        });

            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public void CheckFuelCommand_FuelNotEmpty_NoException()
        {
            Mock<IFuelabel> mockFuelable = new Mock<IFuelabel>();
            mockFuelable.SetupGet<int>(m => m.FuelVolume).Returns(10);
            mockFuelable.SetupGet<int>(m => m.FuelRate).Returns(2);

            bool hasException = false;
            try
            {
                new CheckFuelCommand(mockFuelable.Object).Execute();
            }
            catch (Exception)
            {
                hasException = true;
            }
            //Assert
            Assert.True(hasException == false);
        }
    }
}
