
using SpaceBattleGame.Contracts.Common;

namespace SpaceBattleGame.Server.Common
{    
    public class UObject : IUObject
    {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();

        public object GetProperty(string key)
        {
            if (_properties.ContainsKey(key))
                return _properties[key];
            else
                return null;
        }

        public void SetProperty(string key, object newValue)
        {
            _properties[key] = newValue;
        }
    }
}
