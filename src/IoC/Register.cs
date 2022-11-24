using SpaceBattleGame.Contracts.Commands;

namespace IoC
{
    public class Register : ICommand
    {
        /// <summary>
        /// Зависимости
        /// </summary>
        private Dictionary<string, Func<object[], object>> _dependencies;

        /// <summary>
        /// Функция возвращающая экземпляр объекта
        /// </summary>
        private Func<object[], object> _action;

        /// <summary>
        /// Ключ, по которому возвращается экземпляр объекта
        /// </summary>
        private string _dependencyId;

        public Register(ScopeController scopeController, params object[] parameters)        
        {
            _dependencies = scopeController.GetDependenciesFromCurrentScope();
            _dependencyId = (string)parameters[0];
            _action = (Func<object[], object>)parameters[1];            
        }

        public void Execute()
        {            
            _dependencies[_dependencyId] = _action;
        }        
    }
}
