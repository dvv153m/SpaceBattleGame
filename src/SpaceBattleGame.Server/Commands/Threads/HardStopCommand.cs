using SpaceBattleGame.Contracts.Commands;

namespace SpaceBattleGame.Server.Commands.Threads
{
    /// <summary>
    /// Команда, останавливающая основной цикл обработки команд не дожидаясь их полного завершения
    /// </summary>
    public class HardStopCommand : ICommand
    {
        private Action _stopEventLoop;

        public HardStopCommand(Action stopEventLoop)        
        {            
            _stopEventLoop = stopEventLoop ?? throw new ArgumentNullException(nameof(stopEventLoop));
        }

        public void Execute()
        {            
            _stopEventLoop();
        }
    }
}
