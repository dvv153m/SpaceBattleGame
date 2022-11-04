using Moq;
using SpaceBattleGame.Server.Commands.Move;
using SpaceBattleGame.Server.Commands.Rotate;
using SpaceBattleGame.Server.Common;

namespace SpaceBattleGame.Server.Tests.Commands
{
    public class ChangeVelocityCommandTest
    {
        [Fact]
        public void ChangeVelocityCommand_Move_ChangeVelocity()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            Mock<IRotable> mockRotable = new Mock<IRotable>();

            mockMovable.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            mockMovable.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));

            mockRotable.SetupGet(r => r.Direction).Returns(1);
            mockRotable.SetupGet(r => r.AngularVelocity).Returns(2);
            mockRotable.SetupGet(r => r.DirectionsNumber).Returns(8);

            //Act
            new ChangeVelocityCommand(mockMovable.Object, mockRotable.Object).Execute();

            //Assert
            mockMovable.VerifySet(m => m.Velocity = new Vector(-4, 2));
        }


        [Fact]
        public void ChangeVelocityCommand_NotMove_ChangeVelocity()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            Mock<IRotable> mockRotable = new Mock<IRotable>();

            mockMovable.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            mockMovable.SetupGet(m => m.Velocity).Returns(new Vector(0, 0));

            mockRotable.SetupGet(r => r.Direction).Returns(1);
            mockRotable.SetupGet(r => r.AngularVelocity).Returns(2);
            mockRotable.SetupGet(r => r.DirectionsNumber).Returns(8);

            //Act
            new ChangeVelocityCommand(mockMovable.Object, mockRotable.Object).Execute();

            //Assert
            Assert.Equal(mockMovable.Object.Velocity, new Vector(0, 0));
        }
    }
}
