using Moq;
using SpaceBattleGame.Server.Commands.Rotate;
using SpaceBattleGame.Server.Exceptions;

namespace SpaceBattleGame.Tests
{
    public class RotateCommandTest
    {
        /// <summary>
        /// Объект, находящийся под углом 1 (45 градусов), совершающий поворот с угловой скоростью 2 (90 градусов за раз) направление движения изменяется на 3 (135 градусов)
        /// </summary>
        [Fact]
        public void TryRotate_ChangeDirectionV1_NewDirection()
        {
            //Arrange
            Mock<IRotable> mockRotable = new Mock<IRotable>();
            mockRotable.SetupGet(r => r.Direction).Returns(1);
            mockRotable.SetupGet(r => r.AngularVelocity).Returns(2);
            mockRotable.SetupGet(r => r.DirectionsNumber).Returns(8);

            //Act
            new RotateCommand(mockRotable.Object).Execute();

            //Assert
            mockRotable.VerifySet(r => r.Direction = 3);
        }

        /// <summary>
        /// Объект, находящийся под углом 7 (315 градусов), совершающий поворот с угловой скоростью 3 (135 градусов за раз) направление движения изменяется на 2 (90 градусов)
        /// </summary>
        [Fact]
        public void TryRotate_ChangeDirectionV2_NewDirection()
        {
            //Arrange
            Mock<IRotable> mockRotable = new Mock<IRotable>();
            mockRotable.SetupGet(r => r.Direction).Returns(7);
            mockRotable.SetupGet(r => r.AngularVelocity).Returns(3);
            mockRotable.SetupGet(r => r.DirectionsNumber).Returns(8);

            //Act
            new RotateCommand(mockRotable.Object).Execute();

            //Assert
            mockRotable.VerifySet(r => r.Direction = 2);
        }

        /// <summary>
        /// Попытка повернуть объект, у которого невозможно прочитать направление в пространстве, приводит к ошибке
        /// </summary>
        [Fact]
        public void TryRotate_CanNotReadDirection_ThrowRotateException()
        {
            //Arrange
            Mock<IRotable> mockRotable = new Mock<IRotable>();
            mockRotable.SetupGet(r => r.Direction).Throws(new RotateException("can not read direction"));
            mockRotable.SetupGet(r => r.AngularVelocity).Returns(3);
            mockRotable.SetupGet(r => r.DirectionsNumber).Returns(8);

            //Act            
            var exception = Assert.Throws<RotateException>(
                () =>
                {
                    new RotateCommand(mockRotable.Object).Execute();
                });

            //Assert
            Assert.NotNull(exception);
        }

        /// <summary>
        /// Попытка повернуть объект, у которого невозможно прочитать угловую скорость, приводит к ошибке
        /// </summary>
        [Fact]
        public void TryRotate_CanNotReadAngularVelocity_ThrowRotateException()
        {
            //Arrange
            Mock<IRotable> mockRotable = new Mock<IRotable>();
            mockRotable.SetupGet(r => r.Direction).Returns(1);
            mockRotable.SetupGet(r => r.AngularVelocity).Throws(new RotateException("can not read angular velocity"));
            mockRotable.SetupGet(r => r.DirectionsNumber).Returns(8);

            //Act            
            var exception = Assert.Throws<RotateException>(
                () =>
                {
                    new RotateCommand(mockRotable.Object).Execute();
                });

            //Assert
            Assert.NotNull(exception);
        }

        /// <summary>
        /// Попытка повернуть объект, у которого невозможно прочитать количество направлений, приводит к ошибке
        /// </summary>
        [Fact]
        public void TryRotate_CanNotReadDirectionsNumber_ThrowRotateException()
        {
            //Arrange
            Mock<IRotable> mockRotable = new Mock<IRotable>();
            mockRotable.SetupGet(r => r.Direction).Returns(2);
            mockRotable.SetupGet(r => r.AngularVelocity).Returns(4);
            mockRotable.SetupGet(r => r.DirectionsNumber).Throws(new RotateException("can not read directions number"));

            //Act            
            var exception = Assert.Throws<RotateException>(
                () =>
                {
                    new RotateCommand(mockRotable.Object).Execute();
                });

            //Assert
            Assert.NotNull(exception);
        }

        /// <summary>
        /// Попытка повернуть объект, у которого невозможно изменить направление движения, приводит к ошибке
        /// </summary>
        [Fact]
        public void TryRotate_ObjectDirectionCanNotBeChanged_ThrowRotateException()
        {
            //Arrange
            Mock<IRotable> mockRotable = new Mock<IRotable>();
            mockRotable.SetupSet(r => r.Direction = It.IsAny<int>()).Throws(new RotateException("object can not be change direction"));
            mockRotable.SetupGet(r => r.AngularVelocity).Returns(4);
            mockRotable.SetupGet(r => r.DirectionsNumber).Returns(6);

            //Act            
            var exception = Assert.Throws<RotateException>(
                () =>
                {
                    new RotateCommand(mockRotable.Object).Execute();
                });

            //Assert
            Assert.NotNull(exception);
        }
    }
}
