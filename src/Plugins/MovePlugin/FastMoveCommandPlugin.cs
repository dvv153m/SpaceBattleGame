using IoC;
using SpaceBattleGame.Contracts.Commands;
using SpaceBattleGame.Contracts.Common;

namespace FastMovePlugin
{
    public class FastMoveCommandPlugin : ICommand
    {
        public void Execute()
        {
            IoC.IoC.Resolve<ICommand>("IoC.Register",
                                      "IMovable.Position.Set",
                                      (object[] args) =>
                                      {
                                          return new SetProperty((IUObject)args[0], "Position", (Vector)args[1]);
                                      }).Execute();

            IoC.IoC.Resolve<ICommand>("IoC.Register",
                                      "IMovable.Position.Get",
                                      (object[] args) =>
                                      {
                                          var getPropertyCommand = new GetProperty((IUObject)args[0], "Position");
                                          getPropertyCommand.Execute();
                                          return getPropertyCommand.Value;
                                      }).Execute();

            IoC.IoC.Resolve<ICommand>("IoC.Register",
                                      "IMovable.Velocity.Set",
                                      (object[] args) =>
                                      {
                                          return new SetProperty((IUObject)args[0], "Velocity", (Vector)args[1]);
                                      }).Execute();

            IoC.IoC.Resolve<ICommand>("IoC.Register",
                                      "IMovable.Velocity.Get",
                                      (object[] args) =>
                                      {
                                          var getPropertyCommand = new GetProperty((IUObject)args[0], "Velocity");
                                          getPropertyCommand.Execute();
                                          return getPropertyCommand.Value;
                                      }).Execute();

            IoC.IoC.Resolve<ICommand>("IoC.Register",
                                  "FastMoveCommand",
                                    (object[] args) =>
                                    {
                                        var d = IoC.IoC.Resolve<IFastMovable>("Adapter", typeof(IFastMovable), (IUObject)args[0]);
                                        return new FastMoveCommand(d);
                                    }).Execute();            
        }
    }
}