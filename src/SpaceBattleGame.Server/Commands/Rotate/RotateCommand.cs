
namespace SpaceBattleGame.Server.Commands.Rotate
{
    public class RotateCommand : ICommand
    {
        private IRotable _rotable;

        public RotateCommand(IRotable rotable)
        {
            _rotable = rotable;
        }

        public void Execute()
        {
            _rotable.Direction = (_rotable.Direction + _rotable.AngularVelocity) % _rotable.DirectionsNumber;
        }

        public override string ToString()
        {
            return "RotateCommand";
        }
    }
}
