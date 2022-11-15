using SpaceBattleGame.Contracts.Commands;


namespace FastMovePlugin
{
    public class FastMoveCommand : ICommand
    {
        private IFastMovable _movable;

        public FastMoveCommand(IFastMovable movable)
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
            return "FastMoveCommand";
        }
    }
}
