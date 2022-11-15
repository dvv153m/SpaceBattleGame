using Moq;
using SpaceBattleGame.Contracts.Commands;
using SpaceBattleGame.Contracts.Common;
using SpaceBattleGame.Server.Commands.Move;
using SpaceBattleGame.Server.Common;
using SpaceBattleGame.Server.Exceptions;

namespace SpaceBattleGame.Server.Tests.Commands
{
    public class MoveCommandTest
    {
        /// <summary>
        /// Для объекта, находящегося в точке (12, 5) и движущегося со скоростью (-7, 3) движение меняет положение объекта на (5, 8)
        /// </summary>
        [Fact]
        public void TryMove_ChangePosition_NewPosition()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            mockMovable.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            mockMovable.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));

            //Act
            new MoveCommand(mockMovable.Object).Execute();

            //Assert
            mockMovable.VerifySet(m => m.Position = new Vector(5, 8));
        }

        /// <summary>
        /// Попытка сдвинуть объект, у которого невозможно прочитать положение в пространстве, приводит к ошибке
        /// </summary>
        [Fact]
        public void TryMove_CanNotReadPosition_ShouldThrowMoveException()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            mockMovable.SetupGet(m => m.Position).Throws(new MoveException("can not read position"));
            mockMovable.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));

            //Act            
            var exception = Assert.Throws<MoveException>(
                () =>
                {
                    new MoveCommand(mockMovable.Object).Execute();
                });

            //Assert
            Assert.NotNull(exception);
        }

        /// <summary>
        /// Попытка сдвинуть объект, у которого невозможно прочитать значение мгновенной скорости, приводит к ошибке
        /// </summary>
        [Fact]
        public void TryMove_CanNotReadAngularVelocity_ShouldThrowMoveException()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            mockMovable.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            mockMovable.SetupGet(m => m.Velocity).Throws(new MoveException("can not read angular velocity"));

            //Act            
            var exception = Assert.Throws<MoveException>(
                () =>
                {
                    new MoveCommand(mockMovable.Object).Execute();
                });

            //Assert
            Assert.NotNull(exception);
        }

        /// <summary>
        /// Попытка сдвинуть объект, у которого невозможно изменить положение в пространстве, приводит к ошибке
        /// </summary>
        [Fact]
        public void TryMove_ObjectPositionCannotBeChanged_ShouldThrowMoveException()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            mockMovable.SetupSet<Vector>(m => m.Position = It.IsAny<Vector>()).Throws(new MoveException("object can not be change position"));
            mockMovable.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));

            //Act            
            var exception = Assert.Throws<MoveException>(
                () =>
                {
                    new MoveCommand(mockMovable.Object).Execute();
                });

            //Assert
            Assert.NotNull(exception);
        }
    }
}
