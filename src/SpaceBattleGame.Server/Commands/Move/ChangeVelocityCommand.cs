using SpaceBattleGame.Contracts.Commands;
using SpaceBattleGame.Server.Commands.Rotate;
using SpaceBattleGame.Server.Common;

namespace SpaceBattleGame.Server.Commands.Move
{
    /// <summary>
    /// Команда изменения вектора мгновенной скорости
    /// </summary>
    public class ChangeVelocityCommand : ICommand
    {
        private IMovable _movable;
        private IRotable _rotable;

        public ChangeVelocityCommand(IMovable movable, IRotable rotable)
        {
            _movable = movable;
            _rotable = rotable;
        }

        public void Execute()
        {
            //если об
            if (_movable.Velocity.X == 0 && _movable.Velocity.Y == 0)
            {                
                return;
            }

            int x = (int)(_movable.Velocity.X * Math.Cos(2 * Math.PI / _rotable.DirectionsNumber * _rotable.Direction));
            int y = (int)(_movable.Velocity.Y * Math.Sin(2 * Math.PI / _rotable.DirectionsNumber * _rotable.Direction));
            var newVelocity = new Vector(x, y);

            _movable.Velocity = newVelocity;            
        }
    }
}
