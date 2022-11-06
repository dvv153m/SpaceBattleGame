using Moq;
using SpaceBattleGame.Contracts.Commands;
using SpaceBattleGame.Server.Commands.Fuel;
using SpaceBattleGame.Server.Commands.Macro;
using SpaceBattleGame.Server.Commands.Move;
using SpaceBattleGame.Server.Commands.Rotate;
using SpaceBattleGame.Server.Common;

namespace SpaceBattleGame.Server.Tests.Commands
{
    public class MacroCommandTest
    {
        /// <summary>
        /// Тест на макрокоманду
        /// </summary>
        [Fact]
        public void MacroCommand_Execute_FuelDecreased22()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            Mock<IFuelabel> mockFuelable = new Mock<IFuelabel>();

            mockMovable.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            mockMovable.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));

            mockFuelable.SetupGet<int>(m => m.FuelVolume).Returns(10);
            mockFuelable.SetupGet<int>(m => m.FuelRate).Returns(1);

            var commands = new ICommand[]
            {
                new CheckFuelCommand(mockFuelable.Object),
                new MoveCommand(mockMovable.Object),
                new BurnFuelCommand(mockFuelable.Object)
            };

            //Act
            new MacroCommand(commands).Execute();
            
            mockMovable.VerifySet(m => m.Position = new Vector(5, 8));
            mockFuelable.VerifySet(m => m.FuelVolume = 9);
        }

        /// <summary>
        /// Тест на макрокоманду движения с расходом топлива
        /// </summary>
        [Fact]
        public void MacroCommand_Execute_FuelDecreased()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            Mock<IFuelabel> mockFuelable = new Mock<IFuelabel>();

            mockMovable.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            mockMovable.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));

            mockFuelable.SetupGet<int>(m => m.FuelVolume).Returns(10);
            mockFuelable.SetupGet<int>(m => m.FuelRate).Returns(1);

            var commands = new ICommand[]
            {
                new CheckFuelCommand(mockFuelable.Object),
                new MoveCommand(mockMovable.Object),
                new BurnFuelCommand(mockFuelable.Object)
            };

            //Act
            new MacroCommand(commands).Execute();

            //проверить что выполнятся все 3 команды            
            mockFuelable.VerifySet(m => m.FuelVolume = 9);
        }

        [Fact]
        public void MacroCommand_Execute_FuelDecreased2()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            Mock<IFuelabel> mockFuelable = new Mock<IFuelabel>();
            Mock<IRotable> mockRotable = new Mock<IRotable>();

            mockMovable.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            mockMovable.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));

            mockFuelable.SetupGet<int>(m => m.FuelVolume).Returns(10);
            mockFuelable.SetupGet<int>(m => m.FuelRate).Returns(1);

            mockRotable.SetupGet(r => r.Direction).Returns(1);
            mockRotable.SetupGet(r => r.AngularVelocity).Returns(2);
            mockRotable.SetupGet(r => r.DirectionsNumber).Returns(8);

            var commands = new ICommand[]
            {
                new MoveCommand(mockMovable.Object),
                new RotateCommand(mockRotable.Object),
                new BurnFuelCommand(mockFuelable.Object)
            };

            //Act
            new MacroCommand(commands).Execute();

            //проверить что выполнятся все 3 команды
            mockFuelable.VerifySet(m => m.FuelVolume = 9);
        }
    }
}
