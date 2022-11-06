using SpaceBattleGame.Contracts.Commands;
using SpaceBattleGame.Server.Commands.Move;
using SpaceBattleGame.Server.Exceptions;

namespace SpaceBattleGame.Server.Commands.Rotate
{
    /// <summary>
    /// Команда поворота с сменой вектора мгновенной скорости
    /// </summary>
    public class RotateWithChangeVelocityCommand : ICommand
    {
        private ICommand[] _commands = new ICommand[2];

        public RotateWithChangeVelocityCommand(IMovable movable, IRotable rotable)
        {
            _commands[0] = new RotateCommand(rotable);
            _commands[1] = new ChangeVelocityCommand(movable, rotable);
        }

        public void Execute()
        {
            try
            {
                foreach (var command in _commands)
                {
                    command.Execute();
                }
            }
            catch (Exception ex)
            {
                throw new CommandException(ex.ToString());
            }
        }
    }
}
