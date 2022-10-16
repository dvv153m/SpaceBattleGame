
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
        /// Мгновенная скорость объекта
        /// </summary>
        Vector Velocity { get; set; }        
    }
}
