
using SpaceBattleGame.Server.Common;

namespace SpaceBattleGame.Server.Commands.Move
{
    public interface IMovable
    {
        /// <summary>
        /// Текущая позиция объекта
        /// </summary>
        Vector Position { get; set; }

        /// <summary>
        /// Скорость объекта
        /// </summary>
        Vector Velocity { get; }
    }
}
