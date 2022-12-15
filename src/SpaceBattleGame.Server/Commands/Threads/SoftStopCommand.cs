using SpaceBattleGame.Contracts.Commands;

namespace SpaceBattleGame.Server.Commands.Threads
{
    /// <summary>
    /// Команда, останавливающая основной цикл обработки команд только после того, как все команды завершат свою работу
    /// </summary>
    public class SoftStopCommand : ICommand
    {
        private EventLoopCommand _eventLoopCommand;
        private Action _stopEventLoop;
        
        public SoftStopCommand(EventLoopCommand eventLoopCommand)
        {
            _eventLoopCommand = eventLoopCommand ?? throw new ArgumentNullException(nameof(eventLoopCommand)); ;            
            _stopEventLoop = eventLoopCommand.Stop;            
        }

        private void QueueEmptyHandler(object? sender, EventArgs e)
        {
            _stopEventLoop();
            _eventLoopCommand.QueueEmpty -= QueueEmptyHandler;            
        }

        public void Execute()
        {
            _eventLoopCommand.QueueEmpty += QueueEmptyHandler;            
        }
    }
}
