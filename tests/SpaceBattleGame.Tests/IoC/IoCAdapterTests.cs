using IoC;
using SpaceBattleGame.Contracts.Commands;
using SpaceBattleGame.Contracts.Common;
using SpaceBattleGame.Server.Common;

namespace SpaceBattleGame.Server.Tests
{
    public class IoCAdapterTests
    {
        [Fact]
        public void IoC_AutoGenerateAdapter_MovableAdapter()
        {
            //Arrange
            IUObject uObject = new UObject();
            IoC.IoC.Resolve<ICommand>("IoC.Register", "Adapter", (object[] args) => new Adapter((Type)args[0], (IUObject)args[1]).Instance).Execute();

            //Act                        
            IMovable movableAdapter = IoC.IoC.Resolve<IMovable>("Adapter", typeof(IMovable), uObject);

            //Assert
            Assert.NotNull(movableAdapter);
        }

        [Fact]
        public void IoC_AutoGenerateAdapter_TwoAdapters()
        {
            //Arrange
            IUObject uObject = new UObject();
            IoC.IoC.Resolve<ICommand>("IoC.Register", "Adapter", (object[] args) => new Adapter((Type)args[0], (IUObject)args[1]).Instance).Execute();

            //Act            
            IMovable movableAdapter = IoC.IoC.Resolve<IMovable>("Adapter", typeof(IMovable), uObject);
            IRotable rotableAdapter = IoC.IoC.Resolve<IRotable>("Adapter", typeof(IRotable), uObject);

            //Assert
            Assert.NotNull(movableAdapter);
            Assert.NotNull(rotableAdapter);
        }        
    }
}
