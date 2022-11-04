using SpaceBattleGame.Server.Commands.Fuel;
using SpaceBattleGame.Server.Commands.Move;
using SpaceBattleGame.Server.Commands.Rotate;
using SpaceBattleGame.Server.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpaceBattleGame.Server.Commands.Macro
{
    /// <summary>
    /// Макрокоманда движения с расходом топлива
    /// </summary>
    public class MoveMacroCommand : ICommand
    {
        private IMovable _movable;
        private IFuelabel _fuelabel;

        private ICommand[] _commands = new ICommand[3];

        public MoveMacroCommand(IMovable movable, IFuelabel fuelabel)
        {            
            _movable = movable;
            _fuelabel = fuelabel;

            _commands[0] = new CheckFuelCommand(fuelabel);
            _commands[1] = new MoveCommand(movable);
            _commands[2] = new BurnFuelCommand(fuelabel);
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
