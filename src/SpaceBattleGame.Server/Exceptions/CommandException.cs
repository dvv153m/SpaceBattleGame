using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBattleGame.Server.Exceptions
{
    public class CommandException : Exception
    {
        public CommandException(string message) : base(message)
        {

        }
    }
}
