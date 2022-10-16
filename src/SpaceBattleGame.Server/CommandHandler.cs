using SpaceBattleGame.Server.Commands;
using SpaceBattleGame.Server.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBattleGame.Server
{
    public class CommandHandler
    {
        private readonly Queue<ICommand> _commands = new Queue<ICommand>();
        private ExceptionHandler _exceptionHandler;

        public CommandHandler(ExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler ?? throw new ArgumentNullException(nameof(exceptionHandler));
        }

        public void AddCommand(ICommand command)
        { 
            _commands.Enqueue(command);
        }

        public void Run()
        {
            //todo в отдельном потоке потом запустить
            while (true)
            {
                ICommand command = _commands.Dequeue();
                try
                {
                    command.Execute();
                }
                catch (Exception ex)
                {
                    _exceptionHandler.Handle(command, ex);
                }
            }
        }
    }
}
