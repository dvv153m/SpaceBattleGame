using SpaceBattleGame.Contracts.Commands;
using SpaceBattleGame.Contracts.Common;

namespace IoC
{
    public class GetProperty : ICommand
    {
        private IUObject _obj;
        private string _key;

        public object Value { get; private set; }

        public GetProperty(IUObject obj, string key)
        {
            _obj = obj;
            _key = key;
        }

        public void Execute()
        {
            Value = _obj.GetProperty(_key);
        }
    }
}
