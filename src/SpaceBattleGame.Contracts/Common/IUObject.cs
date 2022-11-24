using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBattleGame.Contracts.Common
{
    public interface IUObject
    {
        object GetProperty(string key);

        void SetProperty(string key, object newValue);
    }
}
