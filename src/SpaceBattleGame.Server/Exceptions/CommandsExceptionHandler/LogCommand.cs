using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceBattleGame.Server.Commands;

namespace SpaceBattleGame.Server.Exceptions.CommandsExceptionHandler
{
    public class LogCommand : ICommand
    {
        private readonly ICommand _command;
        private readonly Exception _exeption;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command">Команда, которая выбросила искоючение</param>
        /// <param name="ex">Исключение кторое выбросила команда cmd</param>
        public LogCommand(ICommand command, Exception exeption)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
            _exeption = exeption ?? throw new ArgumentNullException(nameof(exeption));
        }

        public void Execute()
        {            
            Console.WriteLine($"throw exception in {_command}. Exception details: {_exeption}");
        }
    }
}
