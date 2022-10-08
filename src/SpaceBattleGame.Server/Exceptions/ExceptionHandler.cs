using SpaceBattleGame.Server.Commands;

namespace SpaceBattleGame.Server.Exceptions
{
    /// <summary>
    /// Обработчик исключений
    /// </summary>
    public class ExceptionHandler
    {        
        /// <summary>
        /// Обработччики исключений. key - тип в котором ожидаем исключение; value - Dictionary: key - тип исключения  value - обработчик исключения принимающий ICommand, Exception
        /// </summary>
        private readonly Dictionary<string, Dictionary<Type, Action<ICommand, Exception>>> _handlers = new ();
        
        /// <summary>
        /// Добавление обработчика исключений
        /// </summary>
        /// <param name="command">Тип команды в которой планируем обработать исключение</param>
        /// <param name="exception">Тип исключения, который будем обрабатывать</param>
        /// <param name="action">Обработчик исключения</param>
        public void Setup(Type command, Type exception, Action<ICommand, Exception> action)
        {
            var name = command.ToString();
            if (_handlers.ContainsKey(command.Name))
            {
                _handlers[command.Name].Add(exception, action);
            }
            else
            {
                var exceptions = new Dictionary<Type, Action<ICommand, Exception>>();
                exceptions.Add(exception, action);

                _handlers.Add(command.Name, exceptions);                    
            }            
        }

        public void Handle(ICommand cmd, Exception ex)
        {
            if (cmd != null)
            {                
                var key = cmd.ToString();
                if (_handlers.ContainsKey(key))
                {
                    var cmdExceptionHandler = _handlers[key];
                    var action = cmdExceptionHandler[ex.GetType()];
                    action(cmd, ex);
                }
                else
                {
                    //log не найден обработчик исключения
                }
            }
            //log команда равна null
        }
    }
}
