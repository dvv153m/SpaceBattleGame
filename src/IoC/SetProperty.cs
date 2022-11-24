using SpaceBattleGame.Contracts.Commands;
using SpaceBattleGame.Contracts.Common;

namespace IoC
{
    public class SetProperty : ICommand
    {
        private IUObject _obj;
        private string _key;
        private object _value;

        public SetProperty(IUObject obj, string key, object value)
        {
            _obj = obj;
            _key = key;
            _value = value;
        }

        public void Execute()
        {
            _obj.SetProperty(_key, _value);
        }
    }
}
