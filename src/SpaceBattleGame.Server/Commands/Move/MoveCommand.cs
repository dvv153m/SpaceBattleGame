
namespace SpaceBattleGame.Server.Commands.Move
{
    public class MoveCommand : ICommand
    {
        private IMovable _movable;

        public MoveCommand(IMovable movable)
        {
            _movable = movable ?? throw new ArgumentNullException(nameof(movable));
        }

        public void Execute()
        {            
            if (_movable.Velocity == null)
                throw new ArgumentNullException(nameof(_movable.Velocity));

            _movable.Position += _movable.Velocity;
        }

        public override string ToString()
        {
            return "MoveCommand";
        }
    }
}
