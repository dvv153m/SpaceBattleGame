
namespace SpaceBattleGame.Server.Commands.Move
{
    public class MoveCommand
    {
        private IMovable _movable;

        public MoveCommand(IMovable movable)
        {
            _movable = movable ?? throw new ArgumentNullException(nameof(movable));
        }

        public void Execute()
        {
            _movable.Position += _movable.Velocity;
        }
    }
}
