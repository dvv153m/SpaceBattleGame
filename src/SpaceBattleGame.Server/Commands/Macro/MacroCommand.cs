using SpaceBattleGame.Server.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBattleGame.Server.Commands.Macro
{
    public class MacroCommand : ICommand       //это команда и для движения с топливом и для поворота с изменением вектора мгн скорости
    {
        private ICommand[] _commands;

        public MacroCommand(ICommand[] commands)
        {
            _commands = commands ?? throw new ArgumentNullException();
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
