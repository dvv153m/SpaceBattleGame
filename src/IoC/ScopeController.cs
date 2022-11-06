namespace IoC
{
    public class ScopeController
    {
        private string _currentScopeId;

        public string CurrentScopeId
        {
            get { return _currentScopeId; }
            set
            {
                if (string.IsNullOrEmpty(value)
                    && !_scopes.ContainsKey(value))
                    throw new ArgumentNullException(nameof(CurrentScopeId));

                _currentScopeId = value;
            }
        }

        private Dictionary<string, Dictionary<string, Func<object[], object>>> _scopes = new();

        public ScopeController()
        {
            _currentScopeId = "DefaultScope";
            _scopes.Add(_currentScopeId, new Dictionary<string, Func<object[], object>>());
        }

        public Dictionary<string, Func<object[], object>> GetDependenciesFromCurrentScope()
        {
            return _scopes[_currentScopeId];
        }

        public void Add(string scopeId)
        {
            if (string.IsNullOrEmpty(scopeId))                
                throw new ArgumentNullException(nameof(scopeId));
            if(_scopes.ContainsKey(scopeId))
                throw new ArgumentException(nameof(scopeId));

            _scopes[scopeId] = new();
        }        
    }
}
