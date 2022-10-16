

using SpaceBattleGame.Server.Exceptions;

namespace SpaceBattleGame.Server.Commands.Fuel
{    
    public class BurnFuelCommand : ICommand
    {
        private IFuelabel _fuelabel;

        public BurnFuelCommand(IFuelabel fuelabel)
        {
            _fuelabel = fuelabel ?? throw new ArgumentNullException(nameof(fuelabel));
        }

        public void Execute()
        {
            if (_fuelabel.FuelRate != 0)
            {
                _fuelabel.FuelVolume -= _fuelabel.FuelRate;
            }
            else
                throw new CommandException("fuel rate should not be equal zero");
        }
    }
}
