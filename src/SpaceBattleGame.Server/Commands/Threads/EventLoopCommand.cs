using SpaceBattleGame.Contracts.Commands;
using System.Collections.Concurrent;

namespace SpaceBattleGame.Server.Commands.Threads
{
    /// <summary>
    /// Команда, запускающая основной цикл обработки команд
    /// </summary>
    public class EventLoopCommand : ICommand
    {       
        private bool isStartedThread = false;

        /// <summary>
        /// Запущен ли поток обработки команд
        /// </summary>
        public bool IsStartedThread
        {
            get { return isStartedThread; }
        }

        /// <summary>
        /// Очередь команд
        /// </summary>
        private ConcurrentQueue<ICommand> _commands;

        /// <summary>
        /// Событие, сигнализирующее, что очередь команд пуста
        /// </summary>
        public event EventHandler<EventArgs>? QueueEmpty;

        public EventLoopCommand(ConcurrentQueue<ICommand> commands)
        {
            _commands = commands;
        }

        public void Execute()
        {
            if (!isStartedThread)
            {
                isStartedThread = true;
                Task.Factory.StartNew(() => Consume(), TaskCreationOptions.LongRunning);
            }
        }

        /// <summary>
        /// Флаг, чтоб событие QueueEmpty вызывалось только один раз после того как очередь опустела
        /// </summary>
        private bool _queueEmptyFlag = false;

        public void Consume()
        {
            while (isStartedThread)
            {
                try
                {
                    if (_commands.TryDequeue(out var command))
                    {
                        _queueEmptyFlag = false;
                        command.Execute();                        
                    }
                    else 
                    {
                        if (!_queueEmptyFlag)
                        {
                            QueueEmpty?.Invoke(this, null);
                            _queueEmptyFlag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //loging
                }
            }
        }

        public void Stop()
        {
            isStartedThread = false;
        }

    }
}
