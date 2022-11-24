using SpaceBattleGame.Contracts.Commands;

namespace IoC
{
    public class ScopesNew : ICommand
    {
        private string _scopeId;
        private ScopeController _scopeController;

        public ScopesNew(ScopeController scopeController, params object[] parameters)
        {
            _scopeController = scopeController;
            _scopeId = (string)parameters[0];
        }

        public void Execute()
        {
            _scopeController.Add(_scopeId);
        }
    }
}
