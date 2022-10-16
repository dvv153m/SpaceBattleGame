using SpaceBattleGame.Server.Commands;

namespace SpaceBattleGame.Server.Exceptions.CommandsExceptionHandler
{
    /// <summary>
    /// Повторитель команды
    /// </summary>
    public class TwoCommand : ICommand
    {
        private readonly ICommand _command;

        /// <summary>
        /// Установка команды, которую нужно будет повторить
        /// </summary>
        /// <param name="command">команда, которую нужно будет повторить</param>
        public TwoCommand(ICommand command)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
        }
        public void Execute()
        {
            _command.Execute();
        }

        public override string ToString()
        {
            return "TwoCommand";
        }
    }
}
