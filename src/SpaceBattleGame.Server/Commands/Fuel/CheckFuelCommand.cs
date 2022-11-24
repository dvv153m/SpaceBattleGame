using SpaceBattleGame.Contracts.Commands;
using SpaceBattleGame.Server.Exceptions;

namespace SpaceBattleGame.Server.Commands.Fuel
{
    public class CheckFuelCommand : ICommand
    {
        private IFuelabel _fuelabel;

        public CheckFuelCommand(IFuelabel fuelabel)
        {
            _fuelabel = fuelabel ?? throw new ArgumentNullException(nameof(fuelabel)); 
        }

        public void Execute()
        {            
            if (_fuelabel.FuelVolume - _fuelabel.FuelRate < 0)               
                throw new CommandException("fuel is empty");
        }
    }
}
