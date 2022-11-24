using SpaceBattleGame.Contracts.Commands;

namespace IoC
{
    public class ScopesCurrent : ICommand
    {
        private string _scopeId;

        private ScopeController _scopeController;

        public ScopesCurrent(ScopeController scopeController, params object[] parameters)
        {
            _scopeController = scopeController;
            _scopeId = (string)parameters[0];
        }

        public void Execute()
        {
            _scopeController.CurrentScopeId = _scopeId;
        }
    }
}
