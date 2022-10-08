using SpaceBattleGame.Server.Commands;

namespace SpaceBattleGame.Server.Exceptions.CommandsExceptionHandler
{
    /// <summary>
    /// Повторитель команды
    /// </summary>
    public class OneCommand : ICommand
    {
        private readonly ICommand _command;

        /// <summary>
        /// Установка команды, которую нужно будет повторить
        /// </summary>
        /// <param name="command">команда, которую нужно будет повторить</param>        
        public OneCommand(ICommand command)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
        }

        public void Execute()
        {
            _command.Execute();
        }

        public override string ToString()
        {
            return "OneCommand";
        }
    }
}
