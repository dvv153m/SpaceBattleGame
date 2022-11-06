using Moq;
using SpaceBattleGame.Contracts.Commands;
using SpaceBattleGame.Server.Commands.Move;

namespace SpaceBattleGame.Server.Tests
{
    public class IoCTests
    {
        [Fact]
        public void IoC_Register()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();            

            //Act
            IoC.IoC.Resolve<ICommand>("IoC.Register", "MoveCommand", (object[] args) => new MoveCommand((IMovable)args[0])).Execute();            
            var moveCommand = IoC.IoC.Resolve<ICommand>("MoveCommand", mockMovable.Object);

            //Assert
            Assert.NotNull(moveCommand);
        }

        [Fact]
        public void IoC_Register_WithScopes()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();            

            //Act
            IoC.IoC.Resolve<ICommand>("IoC.ScopesNew", "game1").Execute();            
            IoC.IoC.Resolve<ICommand>("IoC.ScopesCurrent", "game1").Execute();

            IoC.IoC.Resolve<ICommand>("IoC.Register", "MoveCommand", (object[] args) => new MoveCommand((IMovable)args[0])).Execute();
            var moveCommand1 = IoC.IoC.Resolve<ICommand>("MoveCommand", mockMovable.Object);

            IoC.IoC.Resolve<ICommand>("IoC.ScopesNew", "game2").Execute();
            IoC.IoC.Resolve<ICommand>("IoC.ScopesCurrent", "game2").Execute();

            IoC.IoC.Resolve<ICommand>("IoC.Register", "MoveCommand2", (object[] args) => new MoveCommand((IMovable)args[0])).Execute();
            var moveCommand2 = IoC.IoC.Resolve<ICommand>("MoveCommand2", mockMovable.Object);

            //Assert
            Assert.NotNull(moveCommand1);
            Assert.NotNull(moveCommand2);
        }

        [Fact]
        public void IoC_Register_MultiThreading()
        {
            var task1 = Task.Factory.StartNew(() => {

                //Arrange
                Mock<IMovable> mockMovable = new Mock<IMovable>();

                //Act
                IoC.IoC.Resolve<ICommand>("IoC.ScopesNew", "game1").Execute();
                IoC.IoC.Resolve<ICommand>("IoC.ScopesCurrent", "game1").Execute();

                IoC.IoC.Resolve<ICommand>("IoC.Register", "MoveCommand", (object[] args) => new MoveCommand((IMovable)args[0])).Execute();
                var moveCommand = IoC.IoC.Resolve<ICommand>("MoveCommand", mockMovable.Object);

                //Assert
                Assert.NotNull(moveCommand);
            });

            var task2 = Task.Factory.StartNew(() => {

                //Arrange
                Mock<IMovable> mockMovable = new Mock<IMovable>();

                IoC.IoC.Resolve<ICommand>("IoC.ScopesNew", "game1").Execute();
                IoC.IoC.Resolve<ICommand>("IoC.ScopesCurrent", "game1").Execute();

                //Act                
                IoC.IoC.Resolve<ICommand>("IoC.Register", "MoveCommand", (object[] args) => new MoveCommand((IMovable)args[0])).Execute();
                var moveCommand = IoC.IoC.Resolve<ICommand>("MoveCommand", mockMovable.Object);

                //Assert
                Assert.NotNull(moveCommand);
            });

            Task.WaitAll(task1, task2);

            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();

            //Act
            IoC.IoC.Resolve<ICommand>("IoC.Register", "MoveCommand", (object[] args) => new MoveCommand((IMovable)args[0])).Execute();            
            var moveCommand = IoC.IoC.Resolve<ICommand>("MoveCommand", mockMovable.Object);

            //Assert
            Assert.NotNull(moveCommand);
            //проверка что для разных потоковов будет свой MoveCommand (это с помощью ThreadLocal)
        }
    }
}
